using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class ModelViewModelConverter
    {
        public static PlayerViewModel GetPlayerViewModel(Player player)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel
            {
                Id = player.Id,
                Name = player.Name,
                //Balance = player.Balance,
                IsHuman = player.IsHuman
                //Bet = player.Bet
            };
            return playerViewModel;
        }

        public static DealerViewModel GetDealerViewModel(Player dealer)
        {
            DealerViewModel dealerViewModel = new DealerViewModel
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerViewModel;
        }

        public static List<PlayerViewModel> GetBotViewModels(List<Player> bots)
        {
            var botViewModels = new List<PlayerViewModel>();
            foreach (var bot in bots)
            {
                PlayerViewModel botViewModel = new PlayerViewModel
                {
                    Id = bot.Id,
                    Name = bot.Name
                    //Balance = bot.Balance
                };
                botViewModels.Add(botViewModel);
            }
            return botViewModels;
        }

        public static List<HistoryViewModel> GetHistory(List<History> history)
        {
            var historyViewModels = new List<HistoryViewModel>();
            foreach (var entry in history)
            {
                HistoryViewModel historyViewModel = new HistoryViewModel
                {
                    SessionId = entry.SessionId,
                    Time = entry.Time,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                historyViewModels.Add(historyViewModel); 
            }
            return historyViewModels;
        }
    }
}
