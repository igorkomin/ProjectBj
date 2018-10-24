using System.Collections.Generic;
using ProjectBj.Entities.Enums;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseDoubleGameView
    {
        public long SessionId { get; set; }
        public DealerResponseDoubleGameViewItem Dealer { get; set; }
        public PlayerResponseDoubleGameViewItem Player { get; set; }
        public IEnumerable<PlayerResponseDoubleGameViewItem> Bots { get; set; }
    }

    public class DealerResponseDoubleGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseDoubleGameViewItem Hand { get; set; }
    }

    public class PlayerResponseDoubleGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseDoubleGameViewItem Hand { get; set; }
        public ResultResponseDoubleGameViewItem GameResult { get; set; }

        public PlayerResponseDoubleGameViewItem()
        {
            GameResult = new ResultResponseDoubleGameViewItem();
        }
    }

    public class ResultResponseDoubleGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultResponseDoubleGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandResponseDoubleGameViewItem
    {
        public IEnumerable<CardResponseDoubleGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardResponseDoubleGameViewItem
    {
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
    }
}