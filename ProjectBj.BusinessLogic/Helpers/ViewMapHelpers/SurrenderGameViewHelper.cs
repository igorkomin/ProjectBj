using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class SurrenderGameViewHelper
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

        public static HandSurrenderGameViewItem GetHandSurrenderGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardSurrenderGameViewItem> cardSurrenderGameViewItems = GetCardSurrenderGameViewItems(cards);
            var handSurrenderGameViewItem = new HandSurrenderGameViewItem
            {
                Cards = cardSurrenderGameViewItems,
                Score = score
            };
            return handSurrenderGameViewItem;
        }

        private static IEnumerable<CardSurrenderGameViewItem> GetCardSurrenderGameViewItems(IEnumerable<Card> cards)
        {
            var cardSurrenderGameViewItems = new List<CardSurrenderGameViewItem>();
            foreach (var card in cards)
            {
                var cardSurrenderGameViewItem = new CardSurrenderGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardSurrenderGameViewItems.Add(cardSurrenderGameViewItem);
            }
            return cardSurrenderGameViewItems;
        }

        private static PlayerSurrenderGameViewItem GetPlayerSurrenderGameViewItem(Player player)
        {
            var playerSurrenderGameViewItem = new PlayerSurrenderGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerSurrenderGameViewItem;
        }

        private static DealerSurrenderGameViewItem GetDealerSurrenderGameViewItem(Player dealer)
        {
            var dealerSurrenderGameViewItem = new DealerSurrenderGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerSurrenderGameViewItem;
        }

        private static IEnumerable<PlayerSurrenderGameViewItem> GetPlayerSurrenderGameViewItems(IEnumerable<Player> bots)
        {
            var playerSurrenderGameViewItems = new List<PlayerSurrenderGameViewItem>();
            foreach (var bot in bots)
            {
                var playerSurrenderGameViewItem = new PlayerSurrenderGameViewItem
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
