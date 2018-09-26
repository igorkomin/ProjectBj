using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseStartGameView
    {
        public long SessionId { get; set; }
        public DealerStartGameViewItem Dealer { get; set; }
        public PlayerStartGameViewItem Player { get; set; }
        public IEnumerable<PlayerStartGameViewItem> Bots { get; set; }
    }

    public class DealerStartGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandStartGameViewItem Hand { get; set; }
    }

    public class PlayerStartGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public HandStartGameViewItem Hand { get; set; }
        public ResultStartGameViewItem GameResult { get; set; }

        public PlayerStartGameViewItem()
        {
            GameResult = new ResultStartGameViewItem();
        }
    }

    public class ResultStartGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultStartGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandStartGameViewItem
    {
        public IEnumerable<CardStartGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardStartGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}