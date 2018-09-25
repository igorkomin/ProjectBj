using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class HitGameViewHelper
    {
        public static ResponseHitGameView GetHitGameView(long sessionId, Player dealer, Player player, IEnumerable<Player> bots)
        {
            var responseHitGameView = new ResponseHitGameView
            {
                Player = GetPlayerHitGameViewItem(player),
                Dealer = GetDealerHitGameViewItem(dealer),
                Bots = GetPlayerHitGameViewItems(bots),
                SessionId = sessionId
            };
            return responseHitGameView;
        }

        public static HandHitGameViewItem GetHandHitGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardHitGameViewItem> cardItems = GetCardHitGameViewItems(cards);
            var handHitGameViewItem = new HandHitGameViewItem
            {
                Cards = cardItems,
                Score = score
            };
            return handHitGameViewItem;
        }

        private static PlayerHitGameViewItem GetPlayerHitGameViewItem(Player player)
        {
            var playerHitGameViewItem = new PlayerHitGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerHitGameViewItem;
        }

        private static DealerHitGameViewItem GetDealerHitGameViewItem(Player dealer)
        {
            var dealerHitGameViewItem = new DealerHitGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerHitGameViewItem;
        }

        private static IEnumerable<PlayerHitGameViewItem> GetPlayerHitGameViewItems(IEnumerable<Player> bots)
        {
            var playerHitGameViewItems = new List<PlayerHitGameViewItem>();
            foreach (var bot in bots)
            {
                var playerHitGameViewItem = new PlayerHitGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerHitGameViewItems.Add(playerHitGameViewItem);
            }
            return playerHitGameViewItems;
        }

        private static IEnumerable<CardHitGameViewItem> GetCardHitGameViewItems(IEnumerable<Card> cards)
        {
            var cardHitGameViewItems = new List<CardHitGameViewItem>();
            foreach (var card in cards)
            {
                var cardHitGameViewItem = new CardHitGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardHitGameViewItems.Add(cardHitGameViewItem);
            }
            return cardHitGameViewItems;
        }
    }
}
