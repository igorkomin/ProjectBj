using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class HitGameViewHelper
    {
        public static ResponseHitGameView GetHitGameView(int sessionId, Player dealer, Player player, List<Player> bots)
        {
            var responseHitGameView = new ResponseHitGameView
            {
                Player = GetPlayerHitGameViewItem(player),
                Dealer = GetDealerHitGameViewItem(dealer),
                Bots = GetBotsHitGameViewItem(bots),
                SessionId = sessionId
            };
            return responseHitGameView;
        }

        public static HandHitGameViewItem GetHandHitGameViewItem(List<Card> cards, int score)
        {
            List<CardHitGameViewItem> cardItems = GetCardHitGameViewItems(cards);
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

        private static BotsHitGameViewItem GetBotsHitGameViewItem(List<Player> bots)
        {
            BotsHitGameViewItem botsViewItem = new BotsHitGameViewItem();
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
            botsViewItem.Bots = playerHitGameViewItems;
            return botsViewItem;
        }

        private static List<CardHitGameViewItem> GetCardHitGameViewItems(List<Card> cards)
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
