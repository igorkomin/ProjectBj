namespace ProjectBj.BusinessLogic.Helpers
{
    public static class StringHelper
    {
        public static readonly string DealerName = "Dealer";
        public static readonly string ChoseToHitMessage = "chose to hit";
        public static readonly string ChoseToStandMessage = "chose to stand";
        public static readonly string ChoseToDoubleMessage = "chose to double";
        public static readonly string ChoseToSurrenderMessage = "chose to surrender";
        public static readonly string BotsTurnMessage = "Bot players' turn";
        public static readonly string DealerTurnMessage = "Dealer's turn";
        public static readonly string CreatingDeckMessage = "Creating a new deck";
        public static readonly string SavingDeckMessage = "Saving deck to database";
        public static readonly string CreatingHistoryEntryMessage = "Creating game log entry";
        public static readonly string NameReservedMessage = "This name is reserved";
        public static readonly string NoGameToLoadMessage = "No game to load";
        public static readonly string RandomCardsExceptionMessage = "'count' must be more then 0";

        public static string GetGameStartedMessage(long sessionId)
        {
            return $"Game id{sessionId} started";
        }

        public static string GetGameLoadedMessage(long sessionId)
        {
            return $"Game id{sessionId} loaded";
        }

        public static string GetGameEndedMessage(long sessionId)
        {
            return $"Game id{sessionId} ended";
        }

        public static string GetPlayerTakesCardMessage(string cardRank, string cardSuit)
        {
            return $"takes {cardRank} of {cardSuit}";
        }

        public static string GetPlayerIdHitsMessage(long playerId)
        {
            return $"Player id{playerId} {ChoseToHitMessage}";
        }

        public static string GetPlayerIdStandsMessage(long playerId)
        {
            return $"Player id{playerId} {ChoseToStandMessage}";
        }

        public static string GetPlayerIdDoubleDownMessage(long playerId)
        {
            return $"Player id{playerId} {ChoseToDoubleMessage}";
        }
        
        public static string GetPlayerIdSurrenderMessage(long playerId)
        {
            return $"Player id{playerId} {ChoseToSurrenderMessage}";
        }
    }
}