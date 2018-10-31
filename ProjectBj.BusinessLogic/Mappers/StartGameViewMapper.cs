using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.Enums;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class StartGameViewMapper
    {
        public static ResponseStartGameView GetStartGameView(long sessionId, Player dealer, Player player, IEnumerable<Player> bots)
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

        public static HandResponseStartGameViewItem GetHandLoadGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardResponseStartGameViewItem> cardStartGameViewItems = GetCardStartGameViewItems(cards);
            var handStartGameViewItem = new HandResponseStartGameViewItem
            {
                Cards = cardStartGameViewItems,
                Score = score
            };
            return handStartGameViewItem;
        }

        private static IEnumerable<CardResponseStartGameViewItem> GetCardStartGameViewItems(IEnumerable<Card> cards)
        {
            var cardStartGameViewItems = new List<CardResponseStartGameViewItem>();
            foreach (var card in cards)
            {
                var cardStartGameViewItem = new CardResponseStartGameViewItem
                {
                    Suit = (CardSuit) card.Suit,
                    Rank = (CardRank) card.Rank
                };
                cardStartGameViewItems.Add(cardStartGameViewItem);
            }
            return cardStartGameViewItems;
        }

        private static PlayerResponseStartGameViewItem GetPlayerStartGameViewItem(Player player)
        {
            var playerStartGameViewItem = new PlayerResponseStartGameViewItem
            {
                Id = player.Id,
                Name = player.Name
            };
            return playerStartGameViewItem;
        }

        private static DealerResponseStartGameViewItem GetDealerStartGameViewItem(Player dealer)
        {
            var dealerStartGameViewItem = new DealerResponseStartGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerStartGameViewItem;
        }

        private static IEnumerable<PlayerResponseStartGameViewItem> GetPlayerStartGameViewItems(IEnumerable<Player> bots)
        {
            var playerStartGameViewItems = new List<PlayerResponseStartGameViewItem>();
            foreach (var bot in bots)
            {
                var playerStartGameViewItem = new PlayerResponseStartGameViewItem
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
