using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class StartGameViewHelper
    {
        public static ResponseStartGameView GetStartGameView(int sessionId, Player dealer, Player player, List<Player> bots)
        {
            var responseStartGameView = new ResponseStartGameView
            {
                Player = GetPlayerStartGameViewItem(player),
                Dealer = GetDealerStartGameViewItem(dealer),
                Bots = GetPlayerStartGameViewItems(bots),
                SessionId = sessionId
            };
            return responseStartGameView;
        }

        public static HandStartGameViewItem GetHandLoadGameViewItem(List<Card> cards, int score)
        {
            List<CardStartGameViewItem> cardStartGameViewItems = GetCardStartGameViewItems(cards);
            var handStartGameViewItem = new HandStartGameViewItem
            {
                Cards = cardStartGameViewItems,
                Score = score
            };
            return handStartGameViewItem;
        }

        private static List<CardStartGameViewItem> GetCardStartGameViewItems(List<Card> cards)
        {
            var cardStartGameViewItems = new List<CardStartGameViewItem>();
            foreach (var card in cards)
            {
                var cardStartGameViewItem = new CardStartGameViewItem
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardStartGameViewItems.Add(cardStartGameViewItem);
            }
            return cardStartGameViewItems;
        }

        private static PlayerStartGameViewItem GetPlayerStartGameViewItem(Player player)
        {
            var playerStartGameViewItem = new PlayerStartGameViewItem
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerStartGameViewItem;
        }

        private static DealerStartGameViewItem GetDealerStartGameViewItem(Player dealer)
        {
            var dealerStartGameViewItem = new DealerStartGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerStartGameViewItem;
        }

        private static List<PlayerStartGameViewItem> GetPlayerStartGameViewItems(List<Player> bots)
        {
            var playerStartGameViewItems = new List<PlayerStartGameViewItem>();
            foreach (var bot in bots)
            {
                var playerStartGameViewItem = new PlayerStartGameViewItem
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                playerStartGameViewItems.Add(playerStartGameViewItem);
            }
            return playerStartGameViewItems;
        }
    }
}
