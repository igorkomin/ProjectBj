using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.Configuration;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;

namespace ProjectBj.Service
{
    public static class PlayerService
    {
        private static PlayerRepository _playerRepo;
        private static CardRepository _cardRepo;

        static PlayerService()
        {
            _playerRepo = new PlayerRepository();
            _cardRepo = new CardRepository();
        }

        public static Player NewPlayer(string name)
        {
            Player player = new Player { Name = name, Balance = Values.startBalance, InGame = false, IsHuman = true };
            _playerRepo.Create(player);
            player = _playerRepo.Get(player);
            return player;
        }

        public static Player NewBot()
        {
            Player newBot = new Player { Name = Strings.botName, Balance = Values.startBalance, IsHuman = false, InGame = false };
            _playerRepo.Create(newBot);
            newBot = _playerRepo.Get(newBot);
            return newBot;
        }

        public static Player NewDealer()
        {
            Player dealer = new Player { Name = Strings.dealerName, InGame = true, IsHuman = false };
            _playerRepo.Create(dealer);
            dealer = _playerRepo.Get(dealer);
            return dealer;
        }

        public static Player GetDealer()
        {
            Player dealer = PullPlayer(Strings.dealerName);
            if(dealer == null)
            {
                dealer = NewDealer();
            }
            return dealer;
        }

        public static Player PullPlayer(string name)
        {
            Player player = _playerRepo.FindPlayers(name).FirstOrDefault();
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
            Player player = _playerRepo.Get(playerId);
            return player;
        }

        public static List<Card> GetCards(Player player)
        {
            List<Card> cards = _playerRepo.GetCards(player).ToList();
            return cards;
        }

        public static void DeletePlayer(Player player)
        {
            _playerRepo.Delete(player.Id);
        }
    }
}
