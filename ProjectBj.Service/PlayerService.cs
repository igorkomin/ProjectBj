using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.Service.Interfaces;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Enums;

namespace ProjectBj.Service
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

        private Player NewBot()
        {
            Player newBot = new Player { Name = StringHelper.BotName, Balance = ValueHelper.StartBalance, IsHuman = false, InGame = true };
            try
            {
                newBot = _playerRepository.CreateOne(newBot);
                return newBot;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private Player NewDealer()
        {
            Player dealer = new Player { Name = StringHelper.DealerName, InGame = false, IsHuman = false };
            try
            {
                dealer = _playerRepository.CreateOne(dealer);
                return dealer;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public List<Player> CreateBots(int number)
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

        public Player GetDealer()
        {
            Player dealer = PullPlayer(StringHelper.DealerName);
            if(dealer == null)
            {
                dealer = NewDealer();
            }
            return dealer;
        }

        private Player PullPlayer(string name)
        {
            Player player;
            try
            {
                player = _playerRepository.FindPlayers(name).FirstOrDefault();
                return player;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Player GetPlayer(string name)
        {
            Player player = PullPlayer(name);
            if(player == null)
            {
                player = NewPlayer(name);
            }
            return player;
        }

        public Player GetPlayerById(int id)
        {
            Player player;
            try
            {
                player = _playerRepository.Get(id);
                return player;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<Card> GetCards(Player player)
        {
            List<Card> cards;
            try
            {
                cards = _playerRepository.GetCards(player).ToList();
                return cards;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ThrowCards(Player player)
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

        public void ChangePlayerBalance(Player player, int balanceDelta)
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

        public void DeletePlayer(Player player)
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

        private void DeleteAllBots()
        {
            try
            {
                _playerRepository.DeletePlayersByName(StringHelper.BotName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int GetHandTotal(Player player)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = GetCards(player);

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
