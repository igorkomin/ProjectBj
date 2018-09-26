using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseSurrenderGameView
    {
        public long SessionId { get; set; }
        public DealerSurrenderGameViewItem Dealer { get; set; }
        public PlayerSurrenderGameViewItem Player { get; set; }
        public IEnumerable<PlayerSurrenderGameViewItem> Bots { get; set; }
    }

    public class DealerSurrenderGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandSurrenderGameViewItem Hand { get; set; }
    }

    public class PlayerSurrenderGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public HandSurrenderGameViewItem Hand { get; set; }
        public ResultSurrenderGameViewItem GameResult { get; set; }

        public PlayerSurrenderGameViewItem()
        {
            GameResult = new ResultSurrenderGameViewItem();
        }
    }

    public class ResultSurrenderGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultSurrenderGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandSurrenderGameViewItem
    {
        public IEnumerable<CardSurrenderGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardSurrenderGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}