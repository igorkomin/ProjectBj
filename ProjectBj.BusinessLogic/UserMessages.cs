namespace ProjectBj.BusinessLogic
{
    public static class UserMessages
    {
        public const string DealerName = "Dealer";
        public const string ChoseToHitMessage = "chose to hit";
        public const string ChoseToStandMessage = "chose to stand";
        public const string ChoseToDoubleMessage = "chose to double";
        public const string ChoseToSurrenderMessage = "chose to surrender";
        public const string NameReservedMessage = "This name is reserved";
        public const string NoGameToLoadMessage = "No game to load";
        public const string RandomCardsExceptionMessage = "count must be more then 0";
        public const string BotsNumberMustBePositiveMessage = "botsNumber must be 0 or positive";

        public static string GetPlayerTakesCardMessage(string cardRank, string cardSuit)
        {
            return $"takes {cardRank} of {cardSuit}";
        }
    }
}