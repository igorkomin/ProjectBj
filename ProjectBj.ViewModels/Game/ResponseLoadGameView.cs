﻿using ProjectBj.ViewModels.Enums;
using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseLoadGameView
    {
        public long SessionId { get; set; }
        public DealerResponseLoadGameViewItem Dealer { get; set; }
        public PlayerResponseLoadGameViewItem Player { get; set; }
        public IEnumerable<PlayerResponseLoadGameViewItem> Bots { get; set; }
    }

    public class DealerResponseLoadGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseLoadGameViewItem Hand { get; set; }
    }

    public class PlayerResponseLoadGameViewItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HandResponseLoadGameViewItem Hand { get; set; }
        public ResultResponseLoadGameViewItem GameResult { get; set; }

        public PlayerResponseLoadGameViewItem()
        {
            GameResult = new ResultResponseLoadGameViewItem();
        }
    }

    public class ResultResponseLoadGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultResponseLoadGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandResponseLoadGameViewItem
    {
        public IEnumerable<CardResponseLoadGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardResponseLoadGameViewItem
    {
        public CardSuitEnumView Suit { get; set; }
        public CardRankEnumView Rank { get; set; }
    }
}