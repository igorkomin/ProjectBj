using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Common;
using ProjectBj.Common.ExceptionHandlers;
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
            try
            {
                player = _playerRepository.CreateOne(player);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return player;
        }

        private static Player NewBot()
        {
            Player newBot = new Player { Name = AppStrings.BotName, Balance = Values.StartBalance, IsHuman = false, InGame = true };
            try
            {
                newBot = _playerRepository.CreateOne(newBot);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return newBot;
        }

        private static Player NewDealer()
        {
            Player dealer = new Player { Name = AppStrings.DealerName, InGame = false, IsHuman = false };
            try
            {
                dealer = _playerRepository.CreateOne(dealer);
            }
            catch(Exception exception)
            {
                throw exception;
            }

            return dealer;
        }

        public static List<Player> CreateBots(int number)
        {
            DeleteAllBots();
            List<Player> bots = new List<Player>();
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
            Player player;
            try
            {
                player = _playerRepository.FindPlayers(name).FirstOrDefault();
            }
            catch (Exception exception)
            {
                throw exception;
            }
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
            Player player;
            try
            {
                player = _playerRepository.Get(playerId);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return player;
        }

        public static List<Card> GetCards(Player player)
        {
            List<Card> cards;
            try
            {
                cards = _playerRepository.GetCards(player).ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cards;
        }

        public static void ThrowCards(Player player)
        {
            try
            {
                _playerRepository.DeleteCards(player);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void ChangePlayerBalance(Player player, int balanceDelta)
        {
            player.Balance += balanceDelta;
            try
            {
                _playerRepository.Update(player);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePlayer(Player player)
        {
            try
            {
                _playerRepository.Delete(player.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void DeleteAllBots()
        {
            try
            {
                _playerRepository.DeletePlayersByName(AppStrings.BotName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
