using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class HistoryViewMapper
    {
        public static GetGameHistoryHistoryView GetGameHistoryView(IEnumerable<History> history)
        {
            var view = new GetGameHistoryHistoryView();
            var viewItems = new List<EntryGetGameHistoryHistoryViewItem>();
            foreach (var entry in history)
            {
                var item = new EntryGetGameHistoryHistoryViewItem
                {
                    SessionId = entry.SessionId,
                    Time = entry.CreationDate,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                viewItems.Add(item);
            }

            view.Entries = viewItems;

            return view;
        }

        public static GetFullHistoryHistoryView GetFullHistoryView(IEnumerable<History> history)
        {
            var view = new GetFullHistoryHistoryView();
            var viewItems = new List<EntryGetFullHistoryHistoryViewItem>();
            foreach (var entry in history)
            {
                var item = new EntryGetFullHistoryHistoryViewItem
                {
                    SessionId = entry.SessionId,
                    Time = entry.CreationDate,
                    PlayerName = entry.PlayerName,
                    Event = entry.Event
                };
                viewItems.Add(item);
            }

            view.Entries = viewItems;

            return view;
        }
    }
}
