using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.Entities;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Enums;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic
{
    public class PlayerService : IPlayerService
    {
        private PlayerRepository _playerRepository;
        private CardRepository _cardRepository;

        public PlayerService()
        {
            _playerRepository = new PlayerRepository();
            _cardRepository = new CardRepository();
        }

        private async Task<Player> NewPlayer(string name)
        {
            Player player = new Player { Name = name, Balance = ValueHelper.StartBalance, InGame = true, IsHuman = true };
            try
            {
                player = await _playerRepository.CreateOne(player);
                return player;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<Player> NewBot()
        {
            Player newBot = new Player { Name = StringHelper.BotName, Balance = ValueHelper.StartBalance, IsHuman = false, InGame = true };
            try
            {
                newBot = await _playerRepository.CreateOne(newBot);
                return newBot;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<Player> NewDealer()
        {
            Player dealer = new Player { Name = StringHelper.DealerName, InGame = false, IsHuman = false };
            try
            {
                dealer = await _playerRepository.CreateOne(dealer);
                return dealer;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<Player>> CreateBots(int number)
        {
            await DeleteAllBots();
            List<Player> bots = new List<Player>();
            for(int i = 0; i < number; i++)
            {
                Player bot = await NewBot();
                bots.Add(bot);
            }
            return bots;
        }

        public async Task<PlayerViewModel> GetPlayerViewModel(string name)
        {
            Player player = await GetPlayer(name);

            PlayerViewModel playerViewModel = new PlayerViewModel
            {
                Id = player.Id,
                Name = player.Name,
                Balance = player.Balance,
                InGame = player.InGame,
                IsHuman = player.IsHuman
            };

            return playerViewModel;
        }

        public async Task<DealerViewModel> GetDealerViewModel()
        {
            Player dealer = await GetDealer();

            DealerViewModel dealerViewModel = new DealerViewModel
            {
                Id = dealer.Id,
                Name = dealer.Name,
                InGame = dealer.InGame
            };

            return dealerViewModel;
        }

        public async Task<List<PlayerViewModel>> GetBotViewModelList(int botnumber, int sessionId)
        {
            var bots = await _playerRepository.GetSessionBots(sessionId);
            var botViewModels = new List<PlayerViewModel>();

            if (bots.Count == 0 || bots == null)
            {
                bots = await CreateBots(botnumber);
            }

            foreach(var bot in bots)
            {
                PlayerViewModel botViewModel = new PlayerViewModel
                {
                    Id = bot.Id,
                    Name = bot.Name,
                    InGame = bot.InGame,
                    Balance = bot.Balance
                };
                botViewModels.Add(botViewModel);
            }

            return botViewModels;
        }

        public async Task<Player> GetDealer()
        {
            Player dealer = await PullPlayer(StringHelper.DealerName);
            if(dealer == null)
            {
                dealer = await NewDealer();
            }
            return dealer;
        }

        private async Task<Player> PullPlayer(string name)
        {
            try
            {
                var player = await _playerRepository.FindPlayers(name);
                return player.FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<Player> GetPlayer(string name)
        {
            Player player = await PullPlayer(name);
            if(player == null)
            {
                player = await NewPlayer(name);
            }
            return player;
        }

        public async Task<Player> GetPlayerById(int id)
        {
            Player player;
            try
            {
                player = await _playerRepository.Get(id);
                return player;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<Card>> GetCards(int playerId, int sessionId)
        {
            try
            {
                var player = await _playerRepository.Get(playerId);
                var cards = await _playerRepository.GetCards(player, sessionId);
                return cards.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<List<CardViewModel>> GetCardViewModels(int playerId, int sessionId)
        {
            List<Card> cards = await GetCards(playerId, sessionId);
            List<CardViewModel> cardViewModels = new List<CardViewModel>();
            foreach (var card in cards)
            {
                CardViewModel cardViewModel = new CardViewModel
                {
                    Id = card.Id,
                    Suit = card.Suit,
                    Rank = StringHelper.RankName(card.Rank),
                    ImageUrl = StringHelper.CardLink(card.Suit, card.Rank)
                };
                cardViewModels.Add(cardViewModel);
            }
            return cardViewModels;
        }

        public async Task<HandViewModel> GetHandViewModel(int playerId, int sessionId)
        {
            List<CardViewModel> cardViewModels = await GetCardViewModels(playerId, sessionId);
            HandViewModel handViewModel = new HandViewModel
            {
                Cards = cardViewModels,
                Score = await GetHandValue(playerId, sessionId)
            };
            return handViewModel;
        }

        public async Task DeletePlayer(Player player)
        {
            try
            {
                await _playerRepository.Delete(player.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task DeleteAllBots()
        {
            try
            {
                await _playerRepository.DeletePlayersByName(StringHelper.BotName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<int> GetHandValue(int playerId, int sessionId)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = await GetCards(playerId, sessionId);

            foreach (var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;

                if (card.Rank == aceCardRank)
                {
                    totalValue += ValueHelper.AceCardValue;
                    continue;
                }
                if (card.Rank > tenCardRank)
                {
                    totalValue += ValueHelper.FaceCardValue;
                    continue;
                }
                totalValue += card.Rank;
            }

            return totalValue > ValueHelper.BlackjackValue ? totalValue - aceCount * ValueHelper.AceDelta : totalValue;
        }
    }
}
