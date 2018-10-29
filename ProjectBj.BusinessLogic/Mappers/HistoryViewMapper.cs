using ProjectBj.Entities;
using ProjectBj.ViewModels.History;
using System.Collections.Generic;

namespace ProjectBj.BusinessLogic.Mappers
{
    public static class HistoryViewMapper
    {
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
                    PlayerId = entry.PlayerId,
                    Event = entry.Event
                };
                viewItems.Add(item);
            }

            view.Entries = viewItems;

            return view;
        }
    }
}
