using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class GameService : IGameService
    {
        public string _playerName { get; set; }
        public int _botNumber { get; set; }
        private readonly ICardProvider _cardProvider;
        private readonly IHistoryProvider _historyProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IGameSessionProvider _sessionProvider;

        public GameService(ICardProvider cardProvider, IHistoryProvider historyProvider,
            IPlayerProvider playerProvider, IGameSessionProvider sessionProvider)
        {
            _cardProvider = cardProvider;
            _historyProvider = historyProvider;
            _playerProvider = playerProvider;
            _sessionProvider = sessionProvider;
        }

        private async Task<GameViewModel> CreateGameViewModel()
        {
            var player = await _playerProvider.GetPlayerByName(_playerName);
            var session = await _sessionProvider.GetNewSession();
            var bots = await _playerProvider.GetBots(_botNumber, session.Id);
            var dealer = await _playerProvider.GetDealer();

            GameViewModel gameViewModel = new GameViewModel
            {
                Player = ModelViewModelConverter.GetPlayer(player),
                Dealer = ModelViewModelConverter.GetDealer(dealer),
                Bots = ModelViewModelConverter.GetBotPlayers(bots),
                SessionId = session.Id
            };

            return gameViewModel;
        }

        private async Task<GameViewModel> GetGameViewModel(int playerId, int sessionId)
        {
            var player = await _playerProvider.GetPlayerById(playerId);
            var dealer = await _playerProvider.GetDealer();
            var bots = await _playerProvider.GetSessionBots(sessionId);

            var playerViewModel = ModelViewModelConverter.GetPlayer(player);
            var dealerViewModel = ModelViewModelConverter.GetDealer(dealer);
            var botViewModels = ModelViewModelConverter.GetBotPlayers(bots);

            playerViewModel.Hand = await GetHand(player.Id, sessionId);
            dealerViewModel.Hand = await GetHand(dealer.Id, sessionId);

            foreach (var bot in botViewModels)
            {
                bot.Hand = await GetHand(bot.Id, sessionId);
            }

            GameViewModel gameViewModel = new GameViewModel
            {
                Bots = botViewModels,
                Dealer = dealerViewModel,
                Player = playerViewModel,
                SessionId = sessionId
            };
            return gameViewModel;
        }

        private async Task<GameViewModel> UpdateGameResult(int playerId, int sessionId)
        {
            var gameViewModel = await GetGameViewModel(playerId, sessionId);
            var playerScore = gameViewModel.Player.Hand.Score;
            var dealerScore = gameViewModel.Dealer.Hand.Score;
            var playerGameResult = GetGameResult(playerScore, dealerScore);
            gameViewModel.Player.GameResult = (int)playerGameResult;
            gameViewModel.Player.GameResultMessage = playerGameResult.ToString();
            await _historyProvider.CreateHistoryEntry(gameViewModel.Dealer.Name, StringHelper.GetPlayerScoreMessage(dealerScore), sessionId);
            await _historyProvider.CreateHistoryEntry(gameViewModel.Player.Name, StringHelper.GetPlayerScoreMessage(playerScore), sessionId);

            foreach (var bot in gameViewModel.Bots)
            {
                var botScore = bot.Hand.Score;
                var botGameResult = GetGameResult(botScore, dealerScore);
                bot.GameResult = (int)botGameResult;
                bot.GameResultMessage = botGameResult.ToString();
                await _historyProvider.CreateHistoryEntry(bot.Name, StringHelper.GetPlayerScoreMessage(botScore), sessionId);
            }
            return gameViewModel;
        }

        private async Task<GameViewModel> GiveFirstCards()
        {
            GameViewModel gameViewModel = await CreateGameViewModel();
            List<int> playerIds = new List<int>
            {
                gameViewModel.Player.Id,
                gameViewModel.Dealer.Id
            };
            foreach (var bot in gameViewModel.Bots)
            {
                playerIds.Add(bot.Id);
            }
            await GiveFirstTwoCards(playerIds, gameViewModel.SessionId);
            gameViewModel = await GetGameViewModel(gameViewModel.Player.Id, gameViewModel.SessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> GetNewGame(string playerName, int botsNumber)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReservedMessage);
            }
            _playerName = playerName;
            _botNumber = botsNumber;
            var gameViewModel = await GiveFirstCards();
            Log.Info(StringHelper.GetGameStartedMessage(gameViewModel.SessionId));
            return gameViewModel;
        }

        public async Task<GameViewModel> GetUnfinishedGame(string playerName)
        {
            if (playerName == StringHelper.DealerName)
            {
                throw new ArgumentException(StringHelper.NameReservedMessage);
            }
            var player = await _playerProvider.GetPlayerFromDb(playerName);
            var lastSession = await _sessionProvider.GetSessionByPlayerId(player.Id);
            var gameViewModel = await GetGameViewModel(player.Id, lastSession.Id);
            Log.Info(StringHelper.GetGameLoadedMessage(lastSession.Id));
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeHitDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.CreateHistoryEntry(player.Name, StringHelper.ChoseToHitMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdHitsMessage(playerId));
            await GiveCard(playerId, sessionId);
            GameViewModel gameViewModel = await GetGameViewModel(playerId, sessionId);
            int handValue = await GetHandValue(playerId, sessionId);
            if (handValue >= ValueHelper.BlackjackValue)
            {
                gameViewModel = await GetFinalViewModel(playerId, sessionId);
            }
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeStandDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.CreateHistoryEntry(player.Name, StringHelper.ChoseToStandMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdStandsMessage(playerId));
            GameViewModel gameViewModel = await GetFinalViewModel(playerId, sessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeDoubleDownDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.CreateHistoryEntry(player.Name, StringHelper.ChoseToDoubleMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdDoubleDownMessage(playerId));
            await GiveCard(playerId, sessionId);
            GameViewModel gameViewModel = await GetFinalViewModel(playerId, sessionId);
            return gameViewModel;
        }

        public async Task<GameViewModel> MakeSurrenderDecision(int playerId, int sessionId)
        {
            Player player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.CreateHistoryEntry(player.Name, StringHelper.ChoseToSurrenderMessage, sessionId);
            Log.Info(StringHelper.GetPlayerIdSurrenderMessage(playerId));
            await _cardProvider.ClearPlayerHand(playerId, sessionId);
            GameViewModel gameViewModel = await GetFinalViewModel(playerId, sessionId);
            return gameViewModel;
        }

        private async Task<GameViewModel> GetFinalViewModel(int playerId, int sessionId)
        {
            Log.Info(StringHelper.BotsTurnMessage);
            var gameViewModel = await GetGameViewModel(playerId, sessionId);
            await GiveCardsToDealer(gameViewModel.Dealer.Id, sessionId);
            var dealerScore = gameViewModel.Dealer.Hand.Score;

            foreach (var bot in gameViewModel.Bots)
            {
                await GiveCardsToBot(bot.Id, sessionId);
            }

            await CloseGameSession(sessionId);
            gameViewModel = await UpdateGameResult(playerId, sessionId);
            await _playerProvider.DeleteSessionBots(sessionId);
            return gameViewModel;
        }

        private async Task<bool> GiveCardsToBot(int botId, int sessionId)
        {
            int handValue = await GetHandValue(botId, sessionId);
            if (handValue > ValueHelper.MinimumBotHandValue)
            {
                return false;
            }
            await GiveCard(botId, sessionId);
            return await GiveCardsToBot(botId, sessionId);
        }

        private async Task<bool> GiveCardsToDealer(int dealerId, int sessionId)
        {
            Log.Info(StringHelper.DealerTurnMessage);
            int handValue = await GetHandValue(dealerId, sessionId);
            if (handValue > ValueHelper.MinimumDealerHandValue)
            {
                return false;
            }
            await GiveCard(dealerId, sessionId);
            return await GiveCardsToDealer(dealerId, sessionId);
        }

        private async Task GiveFirstTwoCards(List<int> playerIds, int sessionId)
        {
            foreach (var playerId in playerIds)
            {
                await GiveCard(playerId, sessionId);
                await GiveCard(playerId, sessionId);
            }
        }

        private async Task GiveCard(int playerId, int sessionId)
        {
            var deck = await _cardProvider.GetShuffledDeck();
            var card = deck[0];
            var player = await _playerProvider.GetPlayerById(playerId);
            await _historyProvider.CreateHistoryEntry(
                player.Name, StringHelper.GetPlayerTakesCardMessage(
                    EnumHelper.GetCardRankName(card.Rank), card.Suit), sessionId);
            await _playerProvider.GiveCardToPlayer(playerId, sessionId, card.Id);
        }

        private async Task CloseGameSession(int sessionId)
        {
            await _sessionProvider.CloseSession(sessionId);
            Log.Info(StringHelper.GetGameEndedMessage(sessionId));
        }

        private async Task<List<CardPartial>> GetCards(int playerId, int sessionId)
        {
            var player = await _playerProvider.GetPlayerById(playerId);
            var cards = await _cardProvider.GetPlayerCards(playerId, sessionId);
            List<CardPartial> cardViewModels = new List<CardPartial>();
            foreach (var card in cards)
            {
                CardPartial cardViewModel = new CardPartial
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardViewModels.Add(cardViewModel);
            }
            return cardViewModels;
        }

        private async Task<HandPartial> GetHand(int playerId, int sessionId)
        {
            List<CardPartial> cardViewModels = await GetCards(playerId, sessionId);
            HandPartial handViewModel = new HandPartial
            {
                Cards = cardViewModels,
                Score = await GetHandValue(playerId, sessionId)
            };
            return handViewModel;
        }

        private async Task<int> GetHandValue(int playerId, int sessionId)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<CardPartial> cards = await GetCards(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;

                if (card.RankValue == aceCardRank)
                {
                    totalValue += ValueHelper.AceCardValue;
                    aceCount++;
                    continue;
                }
                if (card.RankValue > tenCardRank)
                {
                    totalValue += ValueHelper.FaceCardValue;
                    continue;
                }
                totalValue += card.RankValue;
            }

            return totalValue > ValueHelper.BlackjackValue ?
                totalValue - aceCount * ValueHelper.AceDelta : totalValue;
        }

        private GameResults.Result GetGameResult(int playerScore, int dealerScore)
        {
            //Player player = await _playerProvider.GetPlayerById(playerId);
            //int balanceDelta = bet;
            if (playerScore == ValueHelper.BlackjackValue)
            {
                /*balanceDelta = (bet * 2) + (bet / 2);
                await _logProvider.CreateLogEntry(player.Name,
                    StringHelper.GetWinsMoneyMessage(balanceDelta), sessionId);
                await _playerProvider.ChangePlayerBalance(playerId, balanceDelta);*/
                return GameResults.Result.Blackjack;
            }
            if (playerScore > ValueHelper.BlackjackValue)
            {
                /*balanceDelta = -bet;
                await _logProvider.CreateLogEntry(player.Name,
                    StringHelper.GetLosesMoneyMessage(Math.Abs(balanceDelta)), sessionId);
                await _playerProvider.ChangePlayerBalance(playerId, balanceDelta);*/
                return GameResults.Result.Bust;
            }
            if (playerScore == dealerScore)
            {
                /*await _logProvider.CreateLogEntry(player.Name,
                    StringHelper.GetWinsMoneyMessage(balanceDelta), sessionId);
                await _playerProvider.ChangePlayerBalance(playerId, balanceDelta);*/
                return GameResults.Result.Win;
            }
            if (playerScore == 0)
            {
                /*balanceDelta = bet / 2;
                await _logProvider.CreateLogEntry(player.Name,
                    StringHelper.GetWinsMoneyMessage(balanceDelta), sessionId);
                await _playerProvider.ChangePlayerBalance(playerId, balanceDelta);*/
                return GameResults.Result.Surrender;
            }
            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                /*await _logProvider.CreateLogEntry(player.Name,
                    StringHelper.GetWinsMoneyMessage(balanceDelta), sessionId);
                await _playerProvider.ChangePlayerBalance(playerId, balanceDelta);*/
                return GameResults.Result.Win;
            }
            
            /*balanceDelta = -bet;
            await _logProvider.CreateLogEntry(player.Name,
                StringHelper.GetLosesMoneyMessage(Math.Abs(balanceDelta)), sessionId);
            await _playerProvider.ChangePlayerBalance(playerId, balanceDelta);*/
            return GameResults.Result.Lose;
        }
    }
}