using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
