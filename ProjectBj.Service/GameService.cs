using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.Service.Enums;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service
{
    public class GameService : IGameService
    {
        public int GetHandTotal(Player player)
        {
            int totalValue = 0;
            int aceCount = 0;

            List<Card> cards = new PlayerService().GetCards(player);

            foreach(var card in cards)
            {
                int aceCardRank = (int)CardRanks.Rank.Ace;
                int tenCardRank = (int)CardRanks.Rank.Ten;
                if (card.Rank == aceCardRank)
                {
                    totalValue += ValueHelper.AceCardValue;
                    continue;
                }
                if(card.Rank > tenCardRank)
                {
                    totalValue += ValueHelper.FaceCardValue;
                    continue;
                }
                totalValue += card.Rank;
            }

            return totalValue > ValueHelper.BlackjackValue ? totalValue - aceCount * ValueHelper.AceDelta : totalValue;
        }

        public bool IsBlackjack(int handTotal)
        {
            bool isBlackJack = handTotal == ValueHelper.BlackjackValue ? true : false;
            return isBlackJack;
        }

        public bool IsBust(int handTotal)
        {
            bool isBust = handTotal > ValueHelper.BlackjackValue ? true : false;
            return isBust;
        }

        public void Stay(Player player)
        {
            player.InGame = false;
        }
    }
}
