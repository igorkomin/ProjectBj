using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.StringHelper;
using ProjectBj.ConstantHelper;

namespace ProjectBj.Service
{
    public static class GameService
    {
        public static EFUnitOfWork _database;

        static GameService()
        {
            _database = new EFUnitOfWork();
        }

        public static int GetHandTotal(Player player)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = player.Cards.ToList();

            foreach(var card in cards)
            {
                if(card.Rank == Values.cardValues[12])
                {
                    aceCount++;
                }
                totalValue += card.Value;
            }

            return totalValue > Values.blackjackValue ? totalValue - aceCount * Values.aceDelta : totalValue;
        }

        public static void DealFirstTwoCards(Player dealer, List<Player> players)
        {
            DeckService.DealCard(dealer, true);
            DeckService.DealCard(dealer, true);

            foreach (var player in players)
            {
                DeckService.DealCard(player, false);
                DeckService.DealCard(player, false);
            }
            _database.Save();
        }

        public static void FillDealerHand(Player dealer)
        {
            List<Card> deck = DeckService.GetShuffledDeck();
            foreach (var card in deck)
            {
                int dealerTotal = GetHandTotal(dealer);
                if (dealerTotal > Values.minDealerHandValue)
                {
                    break;
                }
                dealer.Cards.Add(card);
            }
        }

        public static bool IsBlackjack(int handTotal)
        {
            bool isBlackJack = handTotal == Values.blackjackValue ? true : false;
            return isBlackJack;
        }

        public static bool IsBust(int handTotal)
        {
            bool isBust = handTotal > Values.blackjackValue ? true : false;
            return isBust;
        }

        public static void Stay(Player player)
        {
            player.InGame = false;
        }

        public static void Hit(Player player)
        {
            DeckService.DealCard(player, false);
        }
    }
}
