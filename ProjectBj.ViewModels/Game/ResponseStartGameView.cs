using ProjectBj.Entities.Enums;
using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseStartGameView
    {
        public long SessionId { get; set; }
        public DealerResponseStartGameViewItem Dealer { get; set; }
        public PlayerResponseStartGameViewItem Player { get; set; }
        public IEnumerable<PlayerResponseStartGameViewItem> Bots { get; set; }
    }

    public class DealerResponseStartGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseStartGameViewItem Hand { get; set; }
    }

    public class PlayerResponseStartGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public HandResponseStartGameViewItem Hand { get; set; }
        public ResultResponseStartGameViewItem GameResult { get; set; }

        public PlayerResponseStartGameViewItem()
        {
            GameResult = new ResultResponseStartGameViewItem();
        }
    }

    public class ResultResponseStartGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultResponseStartGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandResponseStartGameViewItem
    {
        public IEnumerable<CardResponseStartGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardResponseStartGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
    }
}