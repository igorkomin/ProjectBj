using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Common;
using ProjectBj.Common.ExceptionHandlers;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;

namespace ProjectBj.Service
{
    public static class GameService
    {
        public static int GetHandTotal(Player player)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = PlayerService.GetCards(player);

            foreach(var card in cards)
            {
                int aceCardRank = (int)Enums.CardRanks.Rank.Ace;
                int tenCardRank = (int)Enums.CardRanks.Rank.Ten;
                if (card.Rank == aceCardRank)
                {
                    totalValue += Values.AceCardValue;
                    continue;
                }
                if(card.Rank > tenCardRank)
                {
                    totalValue += Values.FaceCardValue;
                    continue;
                }
                totalValue += card.Rank;
            }
            return totalValue > Values.BlackjackValue ? totalValue - aceCount * Values.AceDelta : totalValue;
        }

        public static void DealFirstTwoCards(List<Player> players)
        {
            foreach (var player in players)
            {
                DealCard(player);
                DealCard(player);
            }
        }

        public static void FillDealerHand(Player dealer)
        {
            List<Card> deck = DeckService.GetShuffledDeck();
            foreach (var card in deck)
            {
                int dealerTotal = GetHandTotal(dealer);
                if (dealerTotal > Values.MinDealerHandValue)
                {
                    return;
                }
                DeckService.GivePlayerCard(dealer, card);
            }
        }

        public static void DealCard(Player player)
        {
            List<Card> deck = DeckService.GetShuffledDeck();
            Card card = deck[0];
            DeckService.GivePlayerCard(player, card);
        }

        public static bool IsBlackjack(int handTotal)
        {
            bool isBlackJack = handTotal == Values.BlackjackValue ? true : false;
            return isBlackJack;
        }

        public static bool IsBust(int handTotal)
        {
            bool isBust = handTotal > Values.BlackjackValue ? true : false;
            return isBust;
        }

        public static void Stay(Player player)
        {
            player.InGame = false;
        }

        public static void Hit(Player player)
        {
            DealCard(player);
        }
    }
}
