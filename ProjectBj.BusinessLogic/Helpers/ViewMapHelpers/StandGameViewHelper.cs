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

        public static HandStandGameViewItem GetHandStandGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardStandGameViewItem> cardStandGameViewItems = GetCardStandGameViewItems(cards);
            var handStandGameViewItem = new HandStandGameViewItem
            {
                Cards = cardStandGameViewItems,
                Score = score
            };
            return handStandGameViewItem;
        }

        private static IEnumerable<CardStandGameViewItem> GetCardStandGameViewItems(IEnumerable<Card> cards)
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

        private static IEnumerable<PlayerStandGameViewItem> GetPlayerStandGameViewItems(IEnumerable<Player> bots)
        {
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
            return playerStandGameViewItems;
        }
    }
}
