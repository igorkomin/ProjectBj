using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class HistoryViewMapper
    {
        public static IEnumerable<GetGameHistoryHistoryView> GetGameHistoryView(IEnumerable<History> history)
        {
            var gameHistoryViews = new List<GetGameHistoryHistoryView>();
            foreach (var entry in history)
            {
                var gameHistoryView = new GetGameHistoryHistoryView
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

        public static IEnumerable<GetFullHistoryHistoryView> GetFullHistoryView(IEnumerable<History> history)
        {
            var fullHistoryViews = new List<GetFullHistoryHistoryView>();
            foreach (var entry in history)
            {
                var fullHistoryView = new GetFullHistoryHistoryView
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
