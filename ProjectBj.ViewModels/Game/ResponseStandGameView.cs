using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseStandGameView
    {
        public long SessionId { get; set; }
        public DealerResponseStandGameViewItem Dealer { get; set; }
        public PlayerResponseStandGameViewItem Player { get; set; }
        public IEnumerable<PlayerResponseStandGameViewItem> Bots { get; set; }
    }

    public class DealerResponseStandGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseStandGameViewItem Hand { get; set; }
    }

    public class PlayerResponseStandGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public HandResponseStandGameViewItem Hand { get; set; }
        public ResultResponseStandGameViewItem GameResult { get; set; }

        public PlayerResponseStandGameViewItem()
        {
            GameResult = new ResultResponseStandGameViewItem();
        }
    }

    public class ResultResponseStandGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultResponseStandGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandResponseStandGameViewItem
    {
        public IEnumerable<CardResponseStandGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardResponseStandGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}