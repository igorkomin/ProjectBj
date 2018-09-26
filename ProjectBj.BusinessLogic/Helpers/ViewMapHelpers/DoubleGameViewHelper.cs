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

        public static ResponseHandDoubleGameViewItem GetHandDoubleGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<ResponseCardDoubleGameViewItem> cardItems = GetCardDoubleGameViewItems(cards);
            var handDoubleGameViewItem = new ResponseHandDoubleGameViewItem
            {
                Cards = cardItems,
                Score = score
            };
            return handDoubleGameViewItem;
        }

        private static IEnumerable<ResponseCardDoubleGameViewItem> GetCardDoubleGameViewItems(IEnumerable<Card> cards)
        {
            var cardDoubleGameViewItems = new List<ResponseCardDoubleGameViewItem>();
            foreach (var card in cards)
            {
                var cardDoubleGameViewItem = new ResponseCardDoubleGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardDoubleGameViewItems.Add(cardDoubleGameViewItem);
            }
            return cardDoubleGameViewItems;
        }

        private static ResponsePlayerDoubleGameViewItem GetPlayerDoubleGameViewItem(Player player)
        {
            var playerDoubleGameViewItem = new ResponsePlayerDoubleGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerDoubleGameViewItem;
        }

        private static ResponseDealerDoubleGameViewItem GetDealerDoubleGameViewItem(Player dealer)
        {
            var dealerDoubleGameViewItem = new ResponseDealerDoubleGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerDoubleGameViewItem;
        }

        private static IEnumerable<ResponsePlayerDoubleGameViewItem> GetPlayerDoubleGameViewItems(IEnumerable<Player> bots)
        {
            var playerDoubleGameViewItems = new List<ResponsePlayerDoubleGameViewItem>();
            
            foreach (var bot in bots)
            {
                var playerDoubleGameViewItem = new ResponsePlayerDoubleGameViewItem
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
