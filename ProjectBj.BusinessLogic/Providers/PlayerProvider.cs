using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using RandomNameGeneratorLibrary;
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

        private async Task<Player> GetNewPlayer(string name)
        {
            Player player = new Player
            {
                Name = name,
                InGame = true,
                IsHuman = true
            };
            player = await _playerRepository.Insert(player);
            return player;
        }

        private async Task<Player> GetNewBot()
        {
            Player bot = new Player
            {
                Name = _nameGenerator.GenerateRandomFirstName(),
                IsHuman = false,
                InGame = true
            };
            bot = await _playerRepository.Insert(bot);
            return bot;
        }

        private async Task<Player> GetNewDealer()
        {
            Player dealer = new Player
            {
                Name = StringHelper.DealerName,
                InGame = false,
                IsHuman = false
            };
            dealer = await _playerRepository.Insert(dealer);
            return dealer;
        }

        private async Task<List<Player>> GetNewBotList(int number)
        {
            List<Player> bots = new List<Player>();
            for(int i = 0; i < number; i++)
            {
                Player bot = await GetNewBot();
                bots.Add(bot);
            }
            return bots;
        }

        public async Task<List<Player>> GetBots(int botnumber, int sessionId)
        {
            var bots = await _playerRepository.GetSessionBots(sessionId);

            if (bots.Count == 0 || bots == null)
            {
                bots = await GetNewBotList(botnumber);
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
            Player dealer = await GetExistingDealer();
            if(dealer == null)
            {
                dealer = await GetNewDealer();
            }
            return dealer;
        }

        public async Task<Player> GetExistingPlayer(string name)
        {
            var searchResults = await _playerRepository.Find(name);
            if (searchResults.Count == 0)
            {
                return null;
            }
            return searchResults.FirstOrDefault();
        }

        private async Task<Player> GetExistingDealer()
        {
            var searchResults = await _playerRepository.Find(StringHelper.DealerName);
            if (searchResults.Count == 0)
            {
                return null;
            }
            return searchResults.FirstOrDefault();
        }

        public async Task<Player> GetPlayerByName(string name)
        {
            Player player = await GetExistingPlayer(name);
            if(player == null)
            {
                player = await GetNewPlayer(name);
            }
            return player;
        }

        public async Task<Player> GetPlayerById(int id)
        {
            Player player = await _playerRepository.GetById(id);
            return player;
        }

        public async Task DeleteSessionBots(int sessionId)
        {
            await _playerRepository.DeleteBotsFromSession(sessionId);
        }

        public async Task GiveCardToPlayer(int playerId, int sessionId, int cardId)
        {
            Player player = await _playerRepository.GetById(playerId);
            await _playerRepository.AddCardToPlayerHand(player, cardId, sessionId);
        }
    }
}
