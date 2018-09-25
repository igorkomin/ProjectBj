using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class DoubleGameViewHelper
    {
        public static ResponseDoubleGameView GetDoubleGameView(long sessionId, Player dealer, Player player, IEnumerable<Player> bots)
        {
            var responseDoubleGameView = new ResponseDoubleGameView
            {
                Player = GetPlayerDoubleGameViewItem(player),
                Dealer = GetDealerDoubleGameViewItem(dealer),
                Bots = GetPlayerDoubleGameViewItems(bots),
                SessionId = sessionId
            };
            return responseDoubleGameView;
        }

        public static HandDoubleGameViewItem GetHandDoubleGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardDoubleGameViewItem> cardItems = GetCardDoubleGameViewItems(cards);
            var handDoubleGameViewItem = new HandDoubleGameViewItem
            {
                Cards = cardItems,
                Score = score
            };
            return handDoubleGameViewItem;
        }

        private static IEnumerable<CardDoubleGameViewItem> GetCardDoubleGameViewItems(IEnumerable<Card> cards)
        {
            var cardDoubleGameViewItems = new List<CardDoubleGameViewItem>();
            foreach (var card in cards)
            {
                var cardDoubleGameViewItem = new CardDoubleGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardDoubleGameViewItems.Add(cardDoubleGameViewItem);
            }
            return cardDoubleGameViewItems;
        }

        private static PlayerDoubleGameViewItem GetPlayerDoubleGameViewItem(Player player)
        {
            var playerDoubleGameViewItem = new PlayerDoubleGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerDoubleGameViewItem;
        }

        private static DealerDoubleGameViewItem GetDealerDoubleGameViewItem(Player dealer)
        {
            var dealerDoubleGameViewItem = new DealerDoubleGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerDoubleGameViewItem;
        }

        private static IEnumerable<PlayerDoubleGameViewItem> GetPlayerDoubleGameViewItems(IEnumerable<Player> bots)
        {
            var playerDoubleGameViewItems = new List<PlayerDoubleGameViewItem>();
            
            foreach (var bot in bots)
            {
                var playerDoubleGameViewItem = new PlayerDoubleGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerDoubleGameViewItems.Add(playerDoubleGameViewItem);
            }
            return playerDoubleGameViewItems;
        }
    }
}
