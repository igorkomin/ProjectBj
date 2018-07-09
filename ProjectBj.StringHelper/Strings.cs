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
        public const string two = "2";
        public const string three = "3";
        public const string four = "4";
        public const string five = "5";
        public const string six = "6";
        public const string seven = "7";
        public const string eight = "8";
        public const string nine = "9";
        public const string ten = "10";
        public const string jack = "Jack";
        public const string queen = "Queen";
        public const string king = "King";
        public const string ace = "Ace";
        #endregion
        #region Names
        public const string dealerName = "Dealer";
        public const string botName = "Bot";
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
