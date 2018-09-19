using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class SurrenderGameViewHelper
    {
        public static ResponseSurrenderGameView GetSurrenderGameView(int sessionId, Player dealer, Player player, List<Player> bots)
        {
            var responseSurrenderGameView = new ResponseSurrenderGameView
            {
                Player = GetPlayerSurrenderGameViewItem(player),
                Dealer = GetDealerSurrenderGameViewItem(dealer),
                Bots = GetBotsSurrenderGameViewItem(bots),
                SessionId = sessionId
            };
            return responseSurrenderGameView;
        }

        public static HandSurrenderGameViewItem GetHandSurrenderGameViewItem(List<Card> cards, int score)
        {
            List<CardSurrenderGameViewItem> cardSurrenderGameViewItems = GetCardSurrenderGameViewItems(cards);
            var handSurrenderGameViewItem = new HandSurrenderGameViewItem
            {
                Cards = cardSurrenderGameViewItems,
                Score = score
            };
            return handSurrenderGameViewItem;
        }

        private static List<CardSurrenderGameViewItem> GetCardSurrenderGameViewItems(List<Card> cards)
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

        private static BotsSurrenderGameViewItem GetBotsSurrenderGameViewItem(List<Player> bots)
        {
            BotsSurrenderGameViewItem botsSurrenderGameViewItem = new BotsSurrenderGameViewItem();
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
            botsSurrenderGameViewItem.Bots = playerSurrenderGameViewItems;
            return botsSurrenderGameViewItem;
        }
    }
}
