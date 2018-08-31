using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.ViewModels.Game
{
    public class LogEntryViewModel
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string Player { get; set; }
        public string Message { get; set; }
    }
}
