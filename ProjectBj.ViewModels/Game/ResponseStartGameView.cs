using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseStartGameView
    {
        public int SessionId { get; set; }
        public DealerStartGameViewItem Dealer { get; set; }
        public PlayerStartGameViewItem Player { get; set; }
        public BotsStartGameViewItem Bots { get; set; }
    }

    public class BotsStartGameViewItem
    {
        public List<PlayerStartGameViewItem> Bots { get; set; }
    }

    public class DealerStartGameViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HandStartGameViewItem Hand { get; set; }
    }

    public class PlayerStartGameViewItem : DealerStartGameViewItem
    {
        public bool IsHuman { get; set; }
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
        public List<CardStartGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardStartGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}