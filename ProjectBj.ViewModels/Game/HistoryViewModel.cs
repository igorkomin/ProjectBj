using System;

namespace ProjectBj.ViewModels.Game
{
    public class HistoryViewModel
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string PlayerName { get; set; }
        public string Event { get; set; }
    }
}
