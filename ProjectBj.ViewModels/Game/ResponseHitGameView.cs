﻿using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class ResponseHitGameView
    {
        public int SessionId { get; set; }
        public DealerHitGameViewItem Dealer { get; set; }
        public PlayerHitGameViewItem Player { get; set; }
        public List<PlayerHitGameViewItem> Bots { get; set; }
    }

    public class DealerHitGameViewItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HandHitGameViewItem Hand { get; set; }
    }

    public class PlayerHitGameViewItem : DealerHitGameViewItem
    {
        public bool IsHuman { get; set; }
        public ResultHitGameViewItem GameResult { get; set; }

        public PlayerHitGameViewItem()
        {
            GameResult = new ResultHitGameViewItem();
        }
    }

    public class ResultHitGameViewItem
    {
        public int State { get; set; }
        public string Result { get; set; }

        public ResultHitGameViewItem()
        {
            State = 0;
            Result = string.Empty;
        }
    }

    public class HandHitGameViewItem
    {
        public List<CardHitGameViewItem> Cards { get; set; }
        public int Score { get; set; }
    }

    public class CardHitGameViewItem
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int RankValue { get; set; }
    }
}