using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseLoadGameView
    {
        public long SessionId { get; set; }
        public DealerLoadGameViewItem Dealer { get; set; }
        public PlayerLoadGameViewItem Player { get; set; }
        public IEnumerable<PlayerLoadGameViewItem> Bots { get; set; }
    }

    public class DealerLoadGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandLoadGameViewItem Hand { get; set; }
    }

    public class PlayerLoadGameViewItem : DealerLoadGameViewItem
    {
        public bool IsHuman { get; set; }
        public ResultLoadGameViewItem GameResult { get; set; }

        public PlayerLoadGameViewItem()
        {
            GameResult = new ResultLoadGameViewItem();
        }
    }

    public class ResultLoadGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultLoadGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandLoadGameViewItem
    {
        public IEnumerable<CardLoadGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardLoadGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}