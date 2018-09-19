﻿using System;

namespace ProjectBj.ViewModels.History
{
    public class GameHistoryView
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string PlayerName { get; set; }
        public string Event { get; set; }
    }
}
