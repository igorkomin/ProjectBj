﻿using ProjectBj.BusinessLogic.Enums;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Game;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Providers
{
    public class PlayerProvider : IPlayerProvider
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly PersonNameGenerator _nameGenerator;

        public PlayerProvider(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
            _nameGenerator = new PersonNameGenerator();
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
                player = await _playerRepository.Create(player);
                playerViewModel = await GetPlayerViewModel(player);
                return playerViewModel;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<PlayerViewModel> NewBot()
        {
            PlayerViewModel botViewModel;
            Player bot = new Player
            {
                Name = _nameGenerator.GenerateRandomFirstAndLastName(),
                Balance = ValueHelper.StartBalance,
                IsHuman = false,
                InGame = true
            };
            try
            {
                bot = await _playerRepository.Create(bot);
                botViewModel = await GetPlayerViewModel(bot);
                return botViewModel;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
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
                dealer = await _playerRepository.Create(dealer);
                dealerViewModel = await GetDealerViewModel(dealer);
                return dealerViewModel;
            }
            catch(Exception exception)
            {
                Log.Error(exception.Message);
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

        public async Task<List<PlayerViewModel>> GetSessionBotViewModels(int sessionId)
        {
            var bots = await _playerRepository.GetSessionBots(sessionId);
            var botViewModels = new List<PlayerViewModel>();

            foreach (var bot in bots)
            {
                PlayerViewModel botViewModel = new PlayerViewModel
                {
                    Id = bot.Id,
                    Name = bot.Name,
                    Balance = bot.Balance
                };
                botViewModels.Add(botViewModel);
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

        public async Task<PlayerViewModel> PullPlayer(string name)
        {
            try
            {
                var searchResults = await _playerRepository.FindPlayers(name);
                if(searchResults.Count == 0)
                {
                    return null;
                }
                var playerViewModel = await GetPlayerViewModel(searchResults.FirstOrDefault());
                return playerViewModel;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<DealerViewModel> PullDealer()
        {
            try
            {
                var searchResults = await _playerRepository.FindPlayers(StringHelper.DealerName);
                if(searchResults.Count == 0)
                {
                    return null;
                }
                var dealerViewModel = await GetDealerViewModel(searchResults.FirstOrDefault());
                return dealerViewModel;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
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

        public async Task<PlayerViewModel> GetPlayerById(int id)
        {
            Player player;
            PlayerViewModel playerViewModel;
            try
            {
                player = await _playerRepository.GetById(id);
                playerViewModel = await GetPlayerViewModel(player);
                return playerViewModel;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<List<CardViewModel>> GetCardViewModels(int playerId, int sessionId)
        {
            try
            {
                Player player = await _playerRepository.GetById(playerId);
                var cards = await _playerRepository.GetCards(player, sessionId);
                List<CardViewModel> cardViewModels = new List<CardViewModel>();
                foreach (var card in cards)
                {
                    CardViewModel cardViewModel = new CardViewModel
                    {
                        Suit = card.Suit,
                        Rank = EnumHelper.GetRankName(card.Rank),
                        RankValue = card.Rank
                    };
                    cardViewModels.Add(cardViewModel);
                }
                return cardViewModels;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
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

        private async Task DeleteAllBots()
        {
            try
            {
                await _playerRepository.DeleteNonHumanPlayers(StringHelper.DealerName);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
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

        public async Task GivePlayerCard(int playerId, int sessionId, int cardId)
        {
            try
            {
                Player player = await _playerRepository.GetById(playerId);
                await _playerRepository.AddCard(player, cardId, sessionId);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        public async Task ChangePlayerBalance(int playerId, int balanceDelta)
        {
            Player player = await _playerRepository.GetById(playerId);
            player.Balance += balanceDelta;
            try
            {
                await _playerRepository.Update(player);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }
    }
}
