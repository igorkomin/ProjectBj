using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseStandGameView
    {
        public int SessionId { get; set; }
        public DealerStandGameViewItem Dealer { get; set; }
        public PlayerStandGameViewItem Player { get; set; }
        public List<PlayerStandGameViewItem> Bots { get; set; }
    }

    public class DealerStandGameViewItem
    {
        public int Id { get; set; }
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
        public List<CardStandGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardStandGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}