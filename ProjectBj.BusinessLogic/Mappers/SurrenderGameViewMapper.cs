﻿using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class SurrenderGameViewMapper
    {
        public static ResponseSurrenderGameView GetSurrenderGameView(long sessionId, Player dealer, Player player, IEnumerable<Player> bots)
        {
            var responseSurrenderGameView = new ResponseSurrenderGameView
            {
                Player = GetPlayerSurrenderGameViewItem(player),
                Dealer = GetDealerSurrenderGameViewItem(dealer),
                Bots = GetPlayerSurrenderGameViewItems(bots),
                SessionId = sessionId
            };
            return responseSurrenderGameView;
        }

        public static HandResponseSurrenderGameViewItem GetHandSurrenderGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardResponseSurrenderGameViewItem> cardSurrenderGameViewItems = GetCardSurrenderGameViewItems(cards);
            var handSurrenderGameViewItem = new HandResponseSurrenderGameViewItem
            {
                Cards = cardSurrenderGameViewItems,
                Score = score
            };
            return handSurrenderGameViewItem;
        }

        private static IEnumerable<CardResponseSurrenderGameViewItem> GetCardSurrenderGameViewItems(IEnumerable<Card> cards)
        {
            var cardSurrenderGameViewItems = new List<CardResponseSurrenderGameViewItem>();
            foreach (var card in cards)
            {
                var cardSurrenderGameViewItem = new CardResponseSurrenderGameViewItem
                {
                    Suit = card.Suit.ToString(),
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = (int)card.Rank
                };
                cardSurrenderGameViewItems.Add(cardSurrenderGameViewItem);
            }
            return cardSurrenderGameViewItems;
        }

        private static PlayerResponseSurrenderGameViewItem GetPlayerSurrenderGameViewItem(Player player)
        {
            var playerSurrenderGameViewItem = new PlayerResponseSurrenderGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerSurrenderGameViewItem;
        }

        private static DealerResponseSurrenderGameViewItem GetDealerSurrenderGameViewItem(Player dealer)
        {
            var dealerSurrenderGameViewItem = new DealerResponseSurrenderGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerSurrenderGameViewItem;
        }

        private static IEnumerable<PlayerResponseSurrenderGameViewItem> GetPlayerSurrenderGameViewItems(IEnumerable<Player> bots)
        {
            var playerSurrenderGameViewItems = new List<PlayerResponseSurrenderGameViewItem>();
            foreach (var bot in bots)
            {
                var playerSurrenderGameViewItem = new PlayerResponseSurrenderGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerSurrenderGameViewItems.Add(playerSurrenderGameViewItem);
            }
            return playerSurrenderGameViewItems;
        }
    }
}
