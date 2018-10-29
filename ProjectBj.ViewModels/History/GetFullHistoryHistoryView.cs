using System;
using System.Collections.Generic;

namespace ProjectBj.ViewModels.History
{
    public class GetFullHistoryHistoryView
    {
        public List<EntryGetFullHistoryHistoryViewItem> Entries { get; set; }

        public GetFullHistoryHistoryView()
        {
            Entries = new List<EntryGetFullHistoryHistoryViewItem>();
        }
    }

    public class EntryGetFullHistoryHistoryViewItem
    {
        public long SessionId { get; set; }
        public DateTime Time { get; set; }
        public long PlayerId { get; set; }
        public string Event { get; set; }
    }
}
