using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Helpers.ViewMapHelpers
{
    public static class HistoryMapHelper
    {
        public static List<GameHistoryView> GetGameHistoryView(List<History> history)
        {
            var gameHistoryViews = new List<GameHistoryView>();
            foreach (var entry in history)
            {
                var gameHistoryView = new GameHistoryView
                {
                    SessionId = entry.SessionId,
                    Time = entry.Time,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                gameHistoryViews.Add(gameHistoryView); 
            }
            return gameHistoryViews;
        }

        public static List<FullHistoryView> GetFullHistoryView(List<History> history)
        {
            var fullHistoryViews = new List<FullHistoryView>();
            foreach (var entry in history)
            {
                var fullHistoryView = new FullHistoryView
                {
                    SessionId = entry.SessionId,
                    Time = entry.Time,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                fullHistoryViews.Add(fullHistoryView);
            }
            return fullHistoryViews;
        }
    }
}
