namespace ProjectBj.BusinessLogic.Helpers
{
    public static class StringHelper
    {
        public static readonly string DealerName = "Dealer";
        public static readonly string ChoseToHit = "chose to hit";
        public static readonly string ChoseToStand = "chose to stand";
        public static readonly string ChoseToDouble = "chose to double";
        public static readonly string ChoseToSurrender = "chose to surrender";
        public static readonly string BotsTurn = "Bot players' turn";
        public static readonly string DealerTurn = "Dealer's turn";
        public static readonly string CreatingDeck = "Creating a new deck";
        public static readonly string PullingDeck = "Pulling deck from database";
        public static readonly string NoDeckInDb = "No cards in database";
        public static readonly string SavingDeck = "Saving deck to database";
        public static readonly string DeckShuffled = "Deck shuffled";
        public static readonly string CreatingLogEntry = "Creating game log entry";
        public static readonly string GettingLogEntry = "Getting game log entry";
        public static readonly string GettingAllLogs = "Getting all game logs";
        public static readonly string NameReserved = "This name is reserved";
        public static readonly string NoGameToLoad = "No game to load";

        public static string GetGameStartedMessage(int sessionId)
        {
            return $"Game id{sessionId} started";
        }

        public static string GetGameLoadedMessage(int sessionId)
        {
            return $"Game id{sessionId} loaded";
        }

        public static string GetGameEndedMessage(int sessionId)
        {
            return $"Game id{sessionId} ended";
        }

        public static string GetPlayerScoreMessage(int score)
        {
            return $"hand's score: {score}";
        }

        public static string GetPlayerTakesCardMessage(string cardRank, string cardSuit)
        {
            return $"takes {cardRank} of {cardSuit}";
        }

        public static string GetPlayerIdHitsMessage(int playerId)
        {
            return $"Player id{playerId} {ChoseToHit}";
        }

        public static string GetPlayerIdStandsMessage(int playerId)
        {
            return $"Player id{playerId} {ChoseToStand}";
        }

        public static string GetPlayerIdDoubleDownMessage(int playerId)
        {
            return $"Player id{playerId} {ChoseToDouble}";
        }

        public static string GetPlayerIdSurrenderMessage(int playerId)
        {
            return $"Player id{playerId} {ChoseToSurrender}";
        }

        public static string GetWinsMoneyMessage(int amount)
        {
            return $"wins {amount}$";
        }

        public static string GetLosesMoneyMessage(int amount)
        {
            return $"loses {amount}$";
        }
    }
}