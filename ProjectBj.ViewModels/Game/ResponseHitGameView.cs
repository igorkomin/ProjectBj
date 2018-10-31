using ProjectBj.ViewModels.Game.Enums;
using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseHitGameView
    {
        public long SessionId { get; set; }
        public DealerResponseHitGameViewItem Dealer { get; set; }
        public PlayerResponseHitGameViewItem Player { get; set; }
        public IEnumerable<PlayerResponseHitGameViewItem> Bots { get; set; }
    }

    public class DealerResponseHitGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseHitGameViewItem Hand { get; set; }
    }

    public class PlayerResponseHitGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseHitGameViewItem Hand { get; set; }
        public ResultResponseHitGameViewItem GameResult { get; set; }

        public PlayerResponseHitGameViewItem()
        {
            GameResult = new ResultResponseHitGameViewItem();
        }
    }

    public class ResultResponseHitGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultResponseHitGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandResponseHitGameViewItem
    {
        public IEnumerable<CardResponseHitGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardResponseHitGameViewItem
    {
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
    }
}