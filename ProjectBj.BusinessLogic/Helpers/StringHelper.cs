using ProjectBj.BusinessLogic.Enums;
namespace ProjectBj.BusinessLogic.Helpers
{
    public static class StringHelper
    {
        #region Names
        public static readonly string DealerName = "Dealer";
        public static readonly string BotName = "Bot"; 
        #endregion
        #region Messages
        public static string PlayerScore(string playerName, int score)
        {
            return $"{playerName} hand's total score: {score}";
        }
        public static string PlayerTakesCard(string playerName, string cardRank, string cardSuit)
        {
            return $"{playerName} takes {cardRank} of {cardSuit}";
        }
        public static string PlayerHits(string playerName)
        {
            return $"{playerName} hits";
        }
        public static string PlayerStands(string playerName)
        {
            return $"{playerName} stands";
        }
        #endregion
        #region Other
        public static string GetRankName(int rankId)
        {
            string rankName = EnumHelper.GetEnumDescription((CardRanks.Rank)rankId);
            return rankName;
        }
        #endregion
    }
}