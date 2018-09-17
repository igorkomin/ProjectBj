using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers
{
    public static class ModelViewModelConverter
    {
        public static PlayerPartial GetPlayer(Entities.Player player)
        {
            PlayerPartial playerViewModel = new ViewModels.Game.PlayerPartial
            {
                Id = player.Id,
                Name = player.Name,
                //Balance = player.Balance,
                IsHuman = player.IsHuman
                //Bet = player.Bet
            };
            return playerViewModel;
        }

        public static DealerPartial GetDealer(Entities.Player dealer)
        {
            DealerPartial dealerViewModel = new DealerPartial
            {
                Id = dealer.Id,
                Name = dealer.Name
            };
            return dealerViewModel;
        }

        public static List<PlayerPartial> GetBotPlayers(List<Entities.Player> bots)
        {
            var botViewModels = new List<PlayerPartial>();
            foreach (var bot in bots)
            {
                PlayerPartial botViewModel = new PlayerPartial
                {
                    Id = bot.Id,
                    Name = bot.Name
                    //Balance = bot.Balance
                };
                botViewModels.Add(botViewModel);
            }
            return botViewModels;
        }

        public static List<HistoryViewModel> GetHistory(List<Entities.History> history)
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
