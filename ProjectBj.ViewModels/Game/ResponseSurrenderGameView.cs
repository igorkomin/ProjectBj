using ProjectBj.Enums;
using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseSurrenderGameView
    {
        public long SessionId { get; set; }
        public DealerResponseSurrenderGameViewItem Dealer { get; set; }
        public PlayerResponseSurrenderGameViewItem Player { get; set; }
        public IEnumerable<PlayerResponseSurrenderGameViewItem> Bots { get; set; }
    }

    public class DealerResponseSurrenderGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseSurrenderGameViewItem Hand { get; set; }
    }

    public class PlayerResponseSurrenderGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseSurrenderGameViewItem Hand { get; set; }
        public ResultResponseSurrenderGameViewItem GameResult { get; set; }

        public PlayerResponseSurrenderGameViewItem()
        {
            GameResult = new ResultResponseSurrenderGameViewItem();
        }
    }

    public class ResultResponseSurrenderGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultResponseSurrenderGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandResponseSurrenderGameViewItem
    {
        public IEnumerable<CardResponseSurrenderGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardResponseSurrenderGameViewItem
    {
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
    }
}