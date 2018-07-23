using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Configuration;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;

namespace ProjectBj.Service
{
    public static class PlayerService
    {
        private static PlayerRepository _playerRepository;
        private static CardRepository _cardRepository;

        static PlayerService()
        {
            _playerRepository = new PlayerRepository();
            _cardRepository = new CardRepository();
        }

        private static Player NewPlayer(string name)
        {
            Player player = new Player { Name = name, Balance = Values.StartBalance, InGame = true, IsHuman = true };
            player = _playerRepository.Create(player);
            return player;
        }

        private static Player NewBot()
        {
            Player newBot = new Player { Name = AppStrings.BotName, Balance = Values.StartBalance, IsHuman = false, InGame = true };
            newBot = _playerRepository.Create(newBot);
            return newBot;
        }

        private static Player NewDealer()
        {
            Player dealer = new Player { Name = AppStrings.DealerName, InGame = false, IsHuman = false };
            dealer = _playerRepository.Create(dealer);
            return dealer;
        }

        public static List<Player> CreateBots(int number)
        {
            DeleteAllBots();
            List<Player> bots = new List<Player>(); ;
            for(int i = 0; i < number; i++)
            {
                Player bot = NewBot();
                bots.Add(bot);
            }
            return bots;
        }

        public static Player GetDealer()
        {
            Player dealer = PullPlayer(AppStrings.DealerName);
            if(dealer == null)
            {
                dealer = NewDealer();
            }
            return dealer;
        }

        private static Player PullPlayer(string name)
        {
            Player player = _playerRepository.FindPlayers(name).FirstOrDefault();
            return player;
        }

        public static Player GetPlayer(string name)
        {
            Player player = PullPlayer(name);
            if(player == null)
            {
                player = NewPlayer(name);
            }
            return player;
        }

        public static Player GetPlayerById(int playerId)
        {
            Player player = _playerRepository.Get(playerId);
            return player;
        }

        public static List<Card> GetCards(Player player)
        {
            List<Card> cards = _playerRepository.GetCards(player).ToList();
            return cards;
        }

        public static void ChangePlayerBalance(Player player, int balanceData)
        {
            player.Balance += balanceData;
            _playerRepository.Update(player);
        }

        public static void DeletePlayer(Player player)
        {
            _playerRepository.Delete(player.Id);
        }

        public static void DeleteAllBots()
        {
            _playerRepository.DeletePlayersByName(AppStrings.BotName);
        }
    }
}
