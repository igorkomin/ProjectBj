namespace ProjectBj.BusinessLogic.Helpers
{
    public static class StringHelper
    {
        public static readonly string DealerName = "Dealer";
        public static readonly string ChoseToHitMessage = "chose to hit";
        public static readonly string ChoseToStandMessage = "chose to stand";
        public static readonly string ChoseToDoubleMessage = "chose to double";
        public static readonly string ChoseToSurrenderMessage = "chose to surrender";
        public static readonly string NameReservedMessage = "This name is reserved";
        public static readonly string NoGameToLoadMessage = "No game to load";
        public static readonly string RandomCardsExceptionMessage = "count must be more then 0";
        public static readonly string BotsNumberMustBePositiveMessage = "botsNumber must be 0 or positive";

        public static string GetPlayerTakesCardMessage(string cardRank, string cardSuit)
        {
            return $"takes {cardRank} of {cardSuit}";
        }
    }
}