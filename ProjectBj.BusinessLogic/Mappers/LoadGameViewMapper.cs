using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class LoadGameViewMapper
    {
        public static ResponseLoadGameView GetLoadGameView(long sessionId, Player dealer, Player player, IEnumerable<Player> bots)
        {
            var responseLoadGameView = new ResponseLoadGameView
            {
                Player = GetPlayerLoadGameViewItem(player),
                Dealer = GetDealerLoadGameViewItem(dealer),
                Bots = GetPlayerLoadGameViewItems(bots),
                SessionId = sessionId
            };
            return responseLoadGameView;
        }

        public static HandResponseLoadGameViewItem GetHandLoadGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardResponseLoadGameViewItem> cardLoadGameViewItems = GetCardLoadGameViewItems(cards);
            var handLoadGameViewItem = new HandResponseLoadGameViewItem
            {
                Cards = cardLoadGameViewItems,
                Score = score
            };
            return handLoadGameViewItem;
        }

        private static IEnumerable<CardResponseLoadGameViewItem> GetCardLoadGameViewItems(IEnumerable<Card> cards)
        {
            var cardLoadGameViewItems = new List<CardResponseLoadGameViewItem>();
            foreach (var card in cards)
            {
                var cardLoadGameViewItem = new CardResponseLoadGameViewItem
                {
                    Suit = card.Suit,
                    Rank = card.Rank
                };
                cardLoadGameViewItems.Add(cardLoadGameViewItem);
            }
            return cardLoadGameViewItems;
        }

        private static PlayerResponseLoadGameViewItem GetPlayerLoadGameViewItem(Player player)
        {
            var playerLoadGameViewItem = new PlayerResponseLoadGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerLoadGameViewItem;
        }

        private static DealerResponseLoadGameViewItem GetDealerLoadGameViewItem(Player dealer)
        {
            var dealerLoadGameViewItem = new DealerResponseLoadGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerLoadGameViewItem;
        }

        private static IEnumerable<PlayerResponseLoadGameViewItem> GetPlayerLoadGameViewItems(IEnumerable<Player> bots)
        {
            var playerLoadGameViewItems = new List<PlayerResponseLoadGameViewItem>();
            foreach (var bot in bots)
            {
                var playerLoadGameViewItem = new PlayerResponseLoadGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerLoadGameViewItems.Add(playerLoadGameViewItem);
            }
            return playerLoadGameViewItems;
        }
    }
}
