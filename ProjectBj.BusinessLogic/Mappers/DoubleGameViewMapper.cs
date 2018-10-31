using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.Game.Enums;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class DoubleGameViewMapper
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

        public static HandResponseDoubleGameViewItem GetHandDoubleGameViewItem(IEnumerable<Card> cards, int score)
        {
            IEnumerable<CardResponseDoubleGameViewItem> cardItems = GetCardDoubleGameViewItems(cards);
            var handDoubleGameViewItem = new HandResponseDoubleGameViewItem
            {
                Cards = cardItems,
                Score = score
            };
            return handDoubleGameViewItem;
        }

        private static IEnumerable<CardResponseDoubleGameViewItem> GetCardDoubleGameViewItems(IEnumerable<Card> cards)
        {
            var cardDoubleGameViewItems = new List<CardResponseDoubleGameViewItem>();
            foreach (var card in cards)
            {
                var cardDoubleGameViewItem = new CardResponseDoubleGameViewItem
                {
                    Suit = (CardSuit)card.Suit,
                    Rank = (CardRank)card.Rank
                };
                cardDoubleGameViewItems.Add(cardDoubleGameViewItem);
            }
            return cardDoubleGameViewItems;
        }

        private static PlayerResponseDoubleGameViewItem GetPlayerDoubleGameViewItem(Player player)
        {
            var playerDoubleGameViewItem = new PlayerResponseDoubleGameViewItem
            {
                Id = player.Id,
                Name = player.Name
            };
            return playerDoubleGameViewItem;
        }

        private static DealerResponseDoubleGameViewItem GetDealerDoubleGameViewItem(Player dealer)
        {
            var dealerDoubleGameViewItem = new DealerResponseDoubleGameViewItem
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerDoubleGameViewItem;
        }

        private static IEnumerable<PlayerResponseDoubleGameViewItem> GetPlayerDoubleGameViewItems(IEnumerable<Player> bots)
        {
            var playerDoubleGameViewItems = new List<PlayerResponseDoubleGameViewItem>();
            
            foreach (var bot in bots)
            {
                var playerDoubleGameViewItem = new PlayerResponseDoubleGameViewItem
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
