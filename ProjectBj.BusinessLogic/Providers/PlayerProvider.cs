using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Logger;
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

        private async Task<Player> NewPlayer(string name)
        {
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
                return player;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<Player> NewBot()
        {
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
                return bot;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<Player> NewDealer()
        {
            Player dealer = new Player
            {
                Name = StringHelper.DealerName,
                InGame = false,
                IsHuman = false
            };
            try
            {
                dealer = await _playerRepository.Create(dealer);
                return dealer;
            }
            catch(Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<List<Player>> CreateBots(int number)
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

        public async Task<List<Player>> GetBots(int botnumber, int sessionId)
        {
            var bots = await _playerRepository.GetSessionBots(sessionId);

            if (bots.Count == 0 || bots == null)
            {
                bots = await CreateBots(botnumber);
            }
            
            return bots.ToList();
        }

        public async Task<List<Player>> GetSessionBots(int sessionId)
        {
            var bots = await _playerRepository.GetSessionBots(sessionId);
            return bots.ToList();
        }

        public async Task<Player> GetDealer()
        {
            Player dealer = await PullDealer();
            if(dealer == null)
            {
                dealer = await NewDealer();
            }
            return dealer;
        }

        public async Task<Player> PullPlayer(string name)
        {
            try
            {
                var searchResults = await _playerRepository.FindPlayers(name);
                if(searchResults.Count == 0)
                {
                    return null;
                }
                return searchResults.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        private async Task<Player> PullDealer()
        {
            try
            {
                var searchResults = await _playerRepository.FindPlayers(StringHelper.DealerName);
                if(searchResults.Count == 0)
                {
                    return null;
                }
                return searchResults.FirstOrDefault();
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
        }

        public async Task<Player> GetPlayerByName(string name)
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
                player = await _playerRepository.GetById(id);
                return player;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                throw exception;
            }
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

        public async Task SetBet(int playerId, int bet)
        {
            Player player = await _playerRepository.GetById(playerId);
            player.Bet = bet;
            await _playerRepository.Update(player);
        }

        public async Task<int> GetBet(int playerId)
        {
            Player player = await _playerRepository.GetById(playerId);
            return player.Bet;
        }
    }
}
