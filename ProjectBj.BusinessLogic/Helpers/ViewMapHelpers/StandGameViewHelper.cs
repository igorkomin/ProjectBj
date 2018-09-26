using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class StandGameViewHelper
    {
        public static ResponseStandGameView GetStandGameView(long sessionId, Player dealer, Player player, IEnumerable<Player> bots)
        {
            var responseStandGameView = new ResponseStandGameView
            {
                Player = GetPlayerStandGameViewItem(player),
                Dealer = GetDealerStandGameViewItem(dealer),
                Bots = GetPlayerStandGameViewItems(bots),
                SessionId = sessionId
            };
            return responseStandGameView;
        }

        public static HandResponseStandGameViewItem GetHandStandGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardResponseStandGameViewItem> cardStandGameViewItems = GetCardStandGameViewItems(cards);
            var handStandGameViewItem = new HandResponseStandGameViewItem
            {
                Cards = cardStandGameViewItems,
                Score = score
            };
            return handStandGameViewItem;
        }

        private static IEnumerable<CardResponseStandGameViewItem> GetCardStandGameViewItems(IEnumerable<Card> cards)
        {
            var cardStandGameViewItems = new List<CardResponseStandGameViewItem>();
            foreach (var card in cards)
            {
                var cardStandGameViewItem = new CardResponseStandGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardStandGameViewItems.Add(cardStandGameViewItem);
            }
            return cardStandGameViewItems;
        }

        private static PlayerResponseStandGameViewItem GetPlayerStandGameViewItem(Player player)
        {
            var playerStandGameViewItem = new PlayerResponseStandGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerStandGameViewItem;
        }

        private static DealerResponseStandGameViewItem GetDealerStandGameViewItem(Player dealer)
        {
            var dealerStandGameViewItem = new DealerResponseStandGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerStandGameViewItem;
        }

        private static IEnumerable<PlayerResponseStandGameViewItem> GetPlayerStandGameViewItems(IEnumerable<Player> bots)
        {
            var playerStandGameViewItems = new List<PlayerResponseStandGameViewItem>();
            foreach (var bot in bots)
            {
                var playerStandGameViewItem = new PlayerResponseStandGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerStandGameViewItems.Add(playerStandGameViewItem);
            }
            return playerStandGameViewItems;
        }
    }
}
