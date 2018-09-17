using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class MapHelper
    {
        public static List<CardInfo> GetCardsInfo(List<Card> cards)
        {
            List<CardInfo> cardInfos = new List<CardInfo>();
            foreach (var card in cards)
            {
                CardInfo cardInfo = new CardInfo
                {
                    Suit = card.Suit,
                    Rank = EnumHelper.GetCardRankName(card.Rank),
                    RankValue = card.Rank
                };
                cardInfos.Add(cardInfo);
            }
            return cardInfos;
        }

        public static PlayerInfo GetPlayerInfo(Player player)
        {
            PlayerInfo playerInfo = new PlayerInfo
            {
                Id = player.Id,
                Name = player.Name,
                IsHuman = player.IsHuman
            };
            return playerInfo;
        }

        public static DealerInfo GetDealerInfo(Player dealer)
        {
            DealerInfo dealerInfo = new DealerInfo
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerInfo;
        }

        public static List<PlayerInfo> GetBotPlayersInfo(List<Player> bots)
        {
            var botPlayersInfo = new List<PlayerInfo>();
            foreach (var bot in bots)
            {
                PlayerInfo botPlayerInfo = new PlayerInfo
                {
                    Id = bot.Id,
                    Name = bot.Name
                };
                botPlayersInfo.Add(botPlayerInfo);
            }
            return botPlayersInfo;
        }

        public static List<HistoryViewModel> GetHistoryInfo(List<History> history)
        {
            var historyInfos = new List<HistoryViewModel>();
            foreach (var entry in history)
            {
                HistoryViewModel historyInfo = new HistoryViewModel
                {
                    SessionId = entry.SessionId,
                    Time = entry.Time,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                historyInfos.Add(historyInfo); 
            }
            return historyInfos;
        }

        public static GameViewModel GetGameViewModel(int sessionId, Player dealer, Player player, List<Player> bots)
        {
            GameViewModel gameViewModel = new GameViewModel
            {
                Player = GetPlayerInfo(player),
                Dealer = GetDealerInfo(dealer),
                Bots = GetBotPlayersInfo(bots),
                SessionId = sessionId
            };
            return gameViewModel;
        }
    }
}
