using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.DAL.Repositories;
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

        public void AddPlayer(Player newPlayer)
        {
            _database.Players.Create(newPlayer);
            _players.Add(newPlayer);
        }

        public void DealFirstTwoCards()
        {
            //_deck.DealCard(_dealer, true);
            //_deck.DealCard(_dealer, true);

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
            bool hasAce = false;

            foreach (var card in cards)
            {
                if(card.Rank == Values.ACE)
                {
                    if (!hasAce)
                        hasAce = true;
                    if (hasAce && totalValue > 21)
                        totalValue -= 10;
                }
                totalValue += card.Value;
            }

            return totalValue;
        }

        public List<Player> GetPlayers()
        {
            return _players;
        }

        public List<Card> GetCards(Player player)
        {
            return player.Cards.ToList();
        }

        public bool IsBlackjack(List <Card> cards)
        {
            return GetHandTotal(cards) == 21 ? true : false;
        }

        public bool IsBust(List <Card> cards)
        {
            return GetHandTotal(cards) > 21 ? true : false;
        }

        public void FillDealerHand()
        {
            while(GetHandTotal(GetCards(_dealer)) < 17)
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
