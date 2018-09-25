using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class HistoryMapHelper
    {
        public static IEnumerable<GameHistoryView> GetGameHistoryView(IEnumerable<History> history)
        {
            var gameHistoryViews = new List<GameHistoryView>();
            foreach (var entry in history)
            {
                var gameHistoryView = new GameHistoryView
                {
                    SessionId = entry.SessionId,
                    Time = entry.CreationDate,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                gameHistoryViews.Add(gameHistoryView); 
            }
            return gameHistoryViews;
        }

        public static IEnumerable<FullHistoryView> GetFullHistoryView(IEnumerable<History> history)
        {
            var fullHistoryViews = new List<FullHistoryView>();
            foreach (var entry in history)
            {
                var fullHistoryView = new FullHistoryView
                {
                    SessionId = entry.SessionId,
                    Time = entry.CreationDate,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                fullHistoryViews.Add(fullHistoryView);
            }
            return fullHistoryViews;
        }
    }
}
