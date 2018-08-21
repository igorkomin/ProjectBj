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

namespace ProjectBj.BusinessLogic.Providers
{
    public class PlayerProvider : IPlayerProvider
    {
        private PlayerRepository _playerRepository;
        private CardRepository _cardRepository;

        public PlayerProvider()
        {
            _playerRepository = new PlayerRepository();
            _cardRepository = new CardRepository();
        }

        private async Task<PlayerViewModel> NewPlayer(string name)
        {
            PlayerViewModel playerViewModel;
            Player player = new Player
            {
                Name = name,
                Balance = ValueHelper.StartBalance,
                InGame = true,
                IsHuman = true
            };
            try
            {
                player = await _playerRepository.CreateOne(player);
                playerViewModel = await GetPlayerViewModel(player);
                return playerViewModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<PlayerViewModel> NewBot()
        {
            PlayerViewModel botViewModel;
            Player bot = new Player
            {
                Name = StringHelper.BotName,
                Balance = ValueHelper.StartBalance,
                IsHuman = false,
                InGame = true
            };
            try
            {
                bot = await _playerRepository.CreateOne(bot);
                botViewModel = await GetPlayerViewModel(bot);
                return botViewModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<DealerViewModel> NewDealer()
        {
            DealerViewModel dealerViewModel;
            Player dealer = new Player
            {
                Name = StringHelper.DealerName,
                InGame = false,
                IsHuman = false
            };
            try
            {
                dealer = await _playerRepository.CreateOne(dealer);
                dealerViewModel = await GetDealerViewModel(dealer);
                return dealerViewModel;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        private async Task<List<PlayerViewModel>> CreateBots(int number)
        {
            await DeleteAllBots();
            List<PlayerViewModel> bots = new List<PlayerViewModel>();
            for(int i = 0; i < number; i++)
            {
                PlayerViewModel bot = await NewBot();
                bots.Add(bot);
            }

            return bots;
        }

        private async Task<PlayerViewModel> GetPlayerViewModel(Player player)
        {
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

        private async Task<DealerViewModel> GetDealerViewModel(Player dealer)
        {
            DealerViewModel dealerViewModel = new DealerViewModel
            {
                Id = dealer.Id,
                Name = dealer.Name,
                InGame = dealer.InGame
            };
            return dealerViewModel;
        }

        public async Task<List<PlayerViewModel>> GetBotViewModels(int botnumber, int sessionId)
        {
            var bots = await _playerRepository.GetSessionBots(sessionId);
            var botViewModels = new List<PlayerViewModel>();

            foreach (var bot in bots)
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

            if (botViewModels.Count == 0 || botViewModels == null)
            {
                botViewModels = await CreateBots(botnumber);
            }
            
            return botViewModels;
        }

        public async Task<DealerViewModel> GetDealer()
        {
            DealerViewModel dealer = await PullDealer();
            if(dealer == null)
            {
                dealer = await NewDealer();
            }
            return dealer;
        }

        private async Task<PlayerViewModel> PullPlayer(string name)
        {
            try
            {
                var player = await _playerRepository.FindPlayers(name);
                var playerViewModel = await GetPlayerViewModel(player.FirstOrDefault());
                return playerViewModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private async Task<DealerViewModel> PullDealer()
        {
            try
            {
                var dealer = await _playerRepository.FindPlayers(StringHelper.DealerName);
                var dealerViewModel = await GetDealerViewModel(dealer.FirstOrDefault());
                return dealerViewModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<PlayerViewModel> GetPlayerViewModel(string name)
        {
            PlayerViewModel player = await PullPlayer(name);
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

        private async Task<List<CardViewModel>> GetCardViewModels(int playerId, int sessionId)
        {
            try
            {
                Player player = await _playerRepository.Get(playerId);
                var cards = await _playerRepository.GetCards(player, sessionId);
                List<CardViewModel> cardViewModels = new List<CardViewModel>();
                foreach (var card in cards)
                {
                    CardViewModel cardViewModel = new CardViewModel
                    {
                        Id = card.Id,
                        Suit = card.Suit,
                        Rank = StringHelper.GetRankName(card.Rank),
                        RankValue = card.Rank,
                    };
                    cardViewModels.Add(cardViewModel);
                }
                return cardViewModels;
            }
            catch (Exception exception)
            {
                throw exception;
            }
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
                await _playerRepository.Delete(player);
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

            List<CardViewModel> cards = await GetCardViewModels(playerId, sessionId);

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
    }
}
