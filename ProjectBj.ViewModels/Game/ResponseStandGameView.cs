using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseStandGameView
    {
        public long SessionId { get; set; }
        public DealerStandGameViewItem Dealer { get; set; }
        public PlayerStandGameViewItem Player { get; set; }
        public IEnumerable<PlayerStandGameViewItem> Bots { get; set; }
    }

    public class DealerStandGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandStandGameViewItem Hand { get; set; }
    }

    public class PlayerStandGameViewItem : DealerStandGameViewItem
    {
        public bool IsHuman { get; set; }
        public ResultStandGameViewItem GameResult { get; set; }

        public PlayerStandGameViewItem()
        {
            GameResult = new ResultStandGameViewItem();
        }
    }

    public class ResultStandGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultStandGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandStandGameViewItem
    {
        public IEnumerable<CardStandGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardStandGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}