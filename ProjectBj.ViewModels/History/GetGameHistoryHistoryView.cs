using System;
using System.Collections.Generic;

namespace ProjectBj.ViewModels.History
{
    public class GetGameHistoryHistoryView
    {
        public List<EntryGetGameHistoryHistoryViewItem> Entries { get; set; }

        public GetGameHistoryHistoryView()
        {
            Entries = new List<EntryGetGameHistoryHistoryViewItem>();
        }
    }

    public class EntryGetGameHistoryHistoryViewItem
    {
        public long SessionId { get; set; }
        public DateTime Time { get; set; }
        public string PlayerName { get; set; }
        public string Event { get; set; }
    }
}
