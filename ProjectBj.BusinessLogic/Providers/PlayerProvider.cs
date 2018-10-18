using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Providers.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
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

        public async Task<IEnumerable<Player>> GetBots(int botnumber, long sessionId)
        {
            IEnumerable<Player> bots = await GetNewBots(botnumber);
            return bots;
        }

        public async Task<IEnumerable<Player>> GetSessionBots(long sessionId)
        {
            IEnumerable<Player> bots = await _playerRepository.GetSessionBots(sessionId);
            return bots;
        }

        public async Task<Player> GetDealer()
        {
            Player dealer = await GetExistingDealer();
            if (dealer == null)
            {
                dealer = await GetNewDealer();
            }
            return dealer;
        }

        public async Task<Player> GetExistingPlayer(string name)
        {
            IEnumerable<Player> searchResults = await _playerRepository.Find(name);
            if (searchResults.Count() == 0)
            {
                return null;
            }
            return searchResults.FirstOrDefault();
        }

        public async Task<Player> GetPlayerByName(string name)
        {
            Player player = await GetExistingPlayer(name);
            if (player == null)
            {
                player = await GetNewPlayer(name);
            }
            return player;
        }

        public async Task<Player> GetPlayerById(long id)
        {
            Player player = await _playerRepository.GetById(id);
            return player;
        }

        public async Task DeleteSessionBots(long sessionId)
        {
            await _playerRepository.DeleteBotsFromSession(sessionId);
        }
        
        public async Task GiveCardsToPlayer(long playerId, long sessionId, IEnumerable<long> cardIds)
        {
            Player player = await _playerRepository.GetById(playerId);
            await _playerRepository.AddCardsToPlayerHand(player, cardIds, sessionId);
        }

        private async Task<Player> GetNewPlayer(string name)
        {
            var player = new Player
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
            var bot = new Player
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
            var dealer = new Player
            {
                Name = StringHelper.DealerName,
                InGame = false,
                IsHuman = false
            };
            dealer = await _playerRepository.Insert(dealer);
            return dealer;
        }

        private async Task<IEnumerable<Player>> GetNewBots(int number)
        {
            var bots = new List<Player>();
            for(int i = 0; i < number; i++)
            {
                Player bot = await GetNewBot();
                bots.Add(bot);
            }
            return bots;
        }

        private async Task<Player> GetExistingDealer()
        {
            IEnumerable<Player> searchResults = await _playerRepository.Find(StringHelper.DealerName);
            if (searchResults.Count() == 0)
            {
                return null;
            }
            return searchResults.FirstOrDefault();
        }
    }
}
