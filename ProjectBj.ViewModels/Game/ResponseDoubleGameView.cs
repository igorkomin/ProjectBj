using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseDoubleGameView
    {
        public int SessionId { get; set; }
        public DealerDoubleGameViewItem Dealer { get; set; }
        public PlayerDoubleGameViewItem Player { get; set; }
        public List<PlayerDoubleGameViewItem> Bots { get; set; }
    }

    public class DealerDoubleGameViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HandDoubleGameViewItem Hand { get; set; }
    }

    public class PlayerDoubleGameViewItem : DealerDoubleGameViewItem
    {
        public bool IsHuman { get; set; }
        public ResultDoubleGameViewItem GameResult { get; set; }

        public PlayerDoubleGameViewItem()
        {
            GameResult = new ResultDoubleGameViewItem();
        }
    }

    public class ResultDoubleGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultDoubleGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandDoubleGameViewItem
    {
        public List<CardDoubleGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardDoubleGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}