using ProjectBj.Service.Enums;
namespace ProjectBj.Service.Helpers
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
            return $"{playerName} has total score: {score}";
        }
        public static string PlayerTakesCard(string playerName, string cardRank, string cardSuit)
        {
            return $"{playerName} takes {cardRank} of {cardSuit}";
        }
        public static string PlayerHits(string playerName)
        {
            return $"{playerName} hits";
        }
        public static string PlayerStays(string playerName)
        {
            return $"{playerName} stays";
        }
        #endregion
        #region Other
        public static string RankName(int rankId)
        {
            string rankName = EnumHelper.GetEnumDescription((CardRanks.Rank)rankId);
            return rankName;
        }
        public static string CardLink(string suit, int rank)
        {
            string cardLink = $"../Content/Cards/{suit}/{rank}.png";
            return cardLink;
        }
        #endregion
    }
}