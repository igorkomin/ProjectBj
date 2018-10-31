using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.Game.Enums;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class HitGameViewMapper
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

        public static HandResponseHitGameViewItem GetHandHitGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardResponseHitGameViewItem> cardItems = GetCardHitGameViewItems(cards);
            var handHitGameViewItem = new HandResponseHitGameViewItem
            {
                Cards = cardItems,
                Score = score
            };
            return handHitGameViewItem;
        }

        private static PlayerResponseHitGameViewItem GetPlayerHitGameViewItem(Player player)
        {
            var playerHitGameViewItem = new PlayerResponseHitGameViewItem
            {
                Id = player.Id,
                Name = player.Name
            };
            return playerHitGameViewItem;
        }

        private static DealerResponseHitGameViewItem GetDealerHitGameViewItem(Player dealer)
        {
            var dealerHitGameViewItem = new DealerResponseHitGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerHitGameViewItem;
        }

        private static IEnumerable<PlayerResponseHitGameViewItem> GetPlayerHitGameViewItems(IEnumerable<Player> bots)
        {
            var playerHitGameViewItems = new List<PlayerResponseHitGameViewItem>();
            foreach (var bot in bots)
            {
                var playerHitGameViewItem = new PlayerResponseHitGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerHitGameViewItems.Add(playerHitGameViewItem);
            }
            return playerHitGameViewItems;
        }

        private static IEnumerable<CardResponseHitGameViewItem> GetCardHitGameViewItems(IEnumerable<Card> cards)
        {
            var cardHitGameViewItems = new List<CardResponseHitGameViewItem>();
            foreach (var card in cards)
            {
                var cardHitGameViewItem = new CardResponseHitGameViewItem
                {
                    Suit = (CardSuit) card.Suit,
                    Rank = (CardRank) card.Rank
                };
                cardHitGameViewItems.Add(cardHitGameViewItem);
            }
            return cardHitGameViewItems;
        }
    }
}
