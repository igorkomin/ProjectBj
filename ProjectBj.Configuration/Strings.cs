using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Configuration
{
    public static class Strings
    {
        #region Card suits
        public static readonly string[] suits = { "Spades", "Clubs", "Hearts", "Diamonds" };
        #endregion
        #region Card values
        public static readonly string two = "2";
        public static readonly string three = "3";
        public static readonly string four = "4";
        public static readonly string five = "5";
        public static readonly string six = "6";
        public static readonly string seven = "7";
        public static readonly string eight = "8";
        public static readonly string nine = "9";
        public static readonly string ten = "10";
        public static readonly string jack = "J";
        public static readonly string queen = "Q";
        public static readonly string king = "K";
        public static readonly string ace = "A";
        #endregion
        #region Names
        public static readonly string dealerName = "Dealer";
        public static readonly string botName = "Bot";
        #endregion
        #region Messages
        public static string PlayerTakesCard(string playerName, int cardRank)
        {
            return $"{playerName} takes {cardRank}";
        }
        #endregion
    }
}
