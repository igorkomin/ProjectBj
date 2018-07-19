using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Configuration
{
    public static class AppStrings
    {
        #region Names
        public static readonly string DealerName = "Dealer";
        public static readonly string BotName = "Bot";
        #endregion
        #region Messages
        public static string PlayerTakesCard(string playerName, int cardRank, string cardSuit)
        {
            return $"{playerName} takes {cardRank} {cardSuit}";
        }
        #endregion
        #region Database
        public static readonly string ConnectionString = "Server=localhost;Database=blackjack;Trusted_Connection=True;";
        #endregion
    }
}
