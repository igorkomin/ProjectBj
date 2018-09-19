using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class LoadGameViewHelper
    {
        public static ResponseLoadGameView GetLoadGameView(int sessionId, Player dealer, Player player, List<Player> bots)
        {
            var responseLoadGameView = new ResponseLoadGameView
            {
                Player = GetPlayerLoadGameViewItem(player),
                Dealer = GetDealerLoadGameViewItem(dealer),
                Bots = GetBotsLoadGameViewItem(bots),
                SessionId = sessionId
            };
            return responseLoadGameView;
        }

        public static HandLoadGameViewItem GetHandLoadGameViewItem(List<Card> cards, int score)
        {
            List<CardLoadGameViewItem> cardLoadGameViewItems = GetCardLoadGameViewItems(cards);
            var handLoadGameViewItem = new HandLoadGameViewItem
            {
                Cards = cardLoadGameViewItems,
                Score = score
            };
            return handLoadGameViewItem;
        }

        private static List<CardLoadGameViewItem> GetCardLoadGameViewItems(List<Card> cards)
        {
            var cardLoadGameViewItems = new List<CardLoadGameViewItem>();
            foreach (var card in cards)
            {
                var cardLoadGameViewItemitem = new CardLoadGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardLoadGameViewItems.Add(cardLoadGameViewItemitem);
            }
            return cardLoadGameViewItems;
        }

        private static PlayerLoadGameViewItem GetPlayerLoadGameViewItem(Player player)
        {
            var playerLoadGameViewItem = new PlayerLoadGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerLoadGameViewItem;
        }

        private static DealerLoadGameViewItem GetDealerLoadGameViewItem(Player dealer)
        {
            var dealerLoadGameViewItem = new DealerLoadGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerLoadGameViewItem;
        }

        private static BotsLoadGameViewItem GetBotsLoadGameViewItem(List<Player> bots)
        {
            BotsLoadGameViewItem botsLoadGameViewItem = new BotsLoadGameViewItem();
            var playerLoadGameViewItems = new List<PlayerLoadGameViewItem>();
            foreach (var bot in bots)
            {
                var playerLoadGameViewItem = new PlayerLoadGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerLoadGameViewItems.Add(playerLoadGameViewItem);
            }
            botsLoadGameViewItem.Bots = playerLoadGameViewItems;
            return botsLoadGameViewItem;
        }
    }
}
