using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Enums;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers
{
    public class PlayerManager : IPlayerManager
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly PersonNameGenerator _nameGenerator;

        public PlayerManager(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
            _nameGenerator = new PersonNameGenerator();
        }

        public async Task<IEnumerable<Player>> GetBots(int botsNumber)
        {
            IEnumerable<Player> bots = await GetExistingBots(botsNumber);
            int existingBotsNumber = bots.Count();
            
            if (existingBotsNumber < Constants.MaximumBots)
            {
                await CreateBots(Constants.MaximumBots - existingBotsNumber);
                return await GetBots(botsNumber);
            }

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
            if (!searchResults.Any())
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
                Type = PlayerType.Player
            };
            player = await _playerRepository.Insert(player);
            return player;
        }

        private async Task<Player> GetNewDealer()
        {
            var dealer = new Player
            {
                Name = PlayerType.Dealer.ToString(),
                Type = PlayerType.Dealer
            };
            dealer = await _playerRepository.Insert(dealer);
            return dealer;
        }
        
        private async Task CreateBots(int number)
        {
            var bots = new List<Player>();
            for (var i = 0; i < number; i++)
            {
                var bot = new Player
                {
                    Name = _nameGenerator.GenerateRandomFirstName(),
                    Type = PlayerType.Bot
                };
                bots.Add(bot);
            }

            await _playerRepository.Insert(bots);
        }

        private async Task<IEnumerable<Player>> GetExistingBots(int number)
        {
            IEnumerable<Player> bots = await _playerRepository.GetBots(number);
            return bots;
        }

        private async Task<Player> GetExistingDealer()
        {
            IEnumerable<Player> players = await _playerRepository.GetByType(PlayerType.Dealer);
            if (!players.Any())
            {
                return null;
            }
            return players.FirstOrDefault();
        }
    }
}
