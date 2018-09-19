﻿using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class StandGameViewHelper
    {
        public static ResponseStandGameView GetStandGameView(int sessionId, Player dealer, Player player, List<Player> bots)
        {
            var responseStandGameView = new ResponseStandGameView
            {
                Player = GetPlayerStandGameViewItem(player),
                Dealer = GetDealerStandGameViewItem(dealer),
                Bots = GetBotsStandGameViewItem(bots),
                SessionId = sessionId
            };
            return responseStandGameView;
        }

        public static HandStandGameViewItem GetHandStandGameViewItem(List<Card> cards, int score)
        {
            List<CardStandGameViewItem> cardStandGameViewItems = GetCardStandGameViewItems(cards);
            var handStandGameViewItem = new HandStandGameViewItem
            {
                Cards = cardStandGameViewItems,
                Score = score
            };
            return handStandGameViewItem;
        }

        private static List<CardStandGameViewItem> GetCardStandGameViewItems(List<Card> cards)
        {
            var cardStandGameViewItems = new List<CardStandGameViewItem>();
            foreach (var card in cards)
            {
                var cardStandGameViewItem = new CardStandGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardStandGameViewItems.Add(cardStandGameViewItem);
            }
            return cardStandGameViewItems;
        }

        private static PlayerStandGameViewItem GetPlayerStandGameViewItem(Player player)
        {
            var playerStandGameViewItem = new PlayerStandGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerStandGameViewItem;
        }

        private static DealerStandGameViewItem GetDealerStandGameViewItem(Player dealer)
        {
            var dealerStandGameViewItem = new DealerStandGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerStandGameViewItem;
        }

        private static BotsStandGameViewItem GetBotsStandGameViewItem(List<Player> bots)
        {
            BotsStandGameViewItem botsStandGameViewItem = new BotsStandGameViewItem();
            var playerStandGameViewItems = new List<PlayerStandGameViewItem>();
            foreach (var bot in bots)
            {
                var playerStandGameViewItem = new PlayerStandGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerStandGameViewItems.Add(playerStandGameViewItem);
            }
            botsStandGameViewItem.Bots = playerStandGameViewItems;
            return botsStandGameViewItem;
        }
    }
}
