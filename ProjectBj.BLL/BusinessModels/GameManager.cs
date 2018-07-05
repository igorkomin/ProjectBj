using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
using ProjectBj.DAL.Utility;
using System.Diagnostics;

namespace ProjectBj.BLL.BusinessModels
{
    public class GameManager
    {
        private Deck _deck;
        private Player _dealer;
        private List<Player> _players;
        private EFUnitOfWork _database;

        public GameManager()
        {
            _database = new EFUnitOfWork();
            _deck = new Deck();
            _dealer = new Player("Dealer", false);
            _players = new List<Player>();
        }

        public Player GetDealer()
        {
            return _dealer;
        }

        public void AddPlayer(Player player)
        {
            _database.Players.Create(player);
            _players.Add(player);
        }

        public void DealFirstTwoCards()
        {
            _deck.DealCard(_dealer, true);
            _deck.DealCard(_dealer, true);

            foreach(var player in _players)
            {
                _deck.DealCard(player, false);
                _deck.DealCard(player, false);
            }
            _database.Save();
        }

        public int GetHandTotal(List<Card> cards)
        {
            int totalValue = 0;
            int aceCount = 0;

            foreach (var card in cards)
            {
                if(card.Rank == Values.ACE)
                {
                    aceCount++;
                }
                totalValue += card.Value;
            }

            return totalValue > 21 ? totalValue - aceCount * 10 : totalValue;
        }

        public List<Player> GetPlayers()
        {
            return DatabaseHelper.SyncPlayers(_players);
        }

        public List<Card> GetCards(Player player)
        { 
            return DatabaseHelper.GetPlayerById(player.Id).Cards.ToList();
        }

        public bool IsBlackjack(int handTotal)
        {
            return handTotal == 21 ? true : false;
        }

        public bool IsBust(int handTotal)
        {
            return handTotal > 21 ? true : false;
        }

        public void FillDealerHand()
        {
            while(GetHandTotal(_dealer.Cards.ToList()) < 17)
            {
                _deck.DealCard(_dealer, true);
            }
        }

        public void Hit(Player player)
        {
            _deck.DealCard(player, false);
        }

        public void Stay(Player player)
        {
            player.InGame = false;
        }
    }
}
