using ProjectBj.BusinessLogic.Enums;
namespace ProjectBj.BusinessLogic.Helpers
{
    public static class StringHelper
    {
        public static readonly string DealerName = "Dealer";
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
        public static readonly string DealerTakesCard = "Dealer takes another card";
        public static readonly string HumanTakesCard = "Human takes another card";


        public static string GameStarted(int sessionId)
        {
            return $"Game id{sessionId} started";
        }

        public static string GameLoaded(int sessionId)
        {
            return $"Game id{sessionId} loaded";
        }

        public static string GameEnded(int sessionId)
        {
            return $"Game id{sessionId} ended";
        }

        public static string GettingSessionLog(int sessionId)
        {
            return $"Getting game id{sessionId} log";
        }

        public static string PlayerScore(string playerName, int score)
        {
            return $"{playerName} hand's total score: {score}";
        }

        public static string PlayerTakesCard(string playerName, string cardRank, string cardSuit)
        {
            return $"{playerName} takes {cardRank} of {cardSuit}";
        }

        public static string PlayerHits(int playerId)
        {
            return $"Player id{playerId} hits";
        }

        public static string PlayerStands(int playerId)
        {
            return $"Player id{playerId} stands";
        }
    }
}