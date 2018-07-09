using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.StringHelper
{
    public static class Strings
    {
        #region Card values
        public static string two = "2";
        public static string three = "3";
        public static string four = "4";
        public static string five = "5";
        public static string six = "6";
        public static string seven = "7";
        public static string eight = "8";
        public static string nine = "9";
        public static string ten = "10";
        public static string jack = "Jack";
        public static string queen = "Queen";
        public static string king = "King";
        public static string ace = "Ace";
        #endregion
        #region Names
        public static string dealerName = "Dealer";
        public static string botName = "Bot";
        #endregion
        #region Messages
        public static string PlayerTakesCard(string playerName, string cardValue)
        {
            return $"Player {playerName} gets {cardValue}";
        }
        public static string DealerTakesCard(string cardValue)
        {
            return $"{dealerName} takes {cardValue}";
        }
        #endregion
    }
}
