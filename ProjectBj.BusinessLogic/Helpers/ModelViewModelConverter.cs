using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class ModelViewModelConverter
    {
        public static PlayerInfo GetPlayer(Player player)
        {
            PlayerInfo playerInfo = new PlayerInfo
            {
                Id = player.Id,
                Name = player.Name,
                //Balance = player.Balance,
                IsHuman = player.IsHuman
                //Bet = player.Bet
            };
            return playerInfo;
        }

        public static DealerInfo GetDealer(Player dealer)
        {
            DealerInfo dealerInfo = new DealerInfo
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerInfo;
        }

        public static List<PlayerInfo> GetBotPlayers(List<Player> bots)
        {
            var botPlayerInfos = new List<PlayerInfo>();
            foreach (var bot in bots)
            {
                PlayerInfo botPlayerInfo = new PlayerInfo
                {
                    Id = bot.Id,
                    Name = bot.Name
                    //Balance = bot.Balance
                };
                botPlayerInfos.Add(botPlayerInfo);
            }
            return botPlayerInfos;
        }

        public static List<HistoryViewModel> GetHistory(List<Entities.History> history)
        {
            var historyInfos = new List<HistoryViewModel>();
            foreach (var entry in history)
            {
                HistoryViewModel historyViewModel = new HistoryViewModel
                {
                    SessionId = entry.SessionId,
                    Time = entry.Time,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                historyInfos.Add(historyViewModel); 
            }
            return historyInfos;
        }
    }
}
