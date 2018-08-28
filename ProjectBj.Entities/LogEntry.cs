using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    [Table("Logs")]
    public class LogEntry : BaseEntity
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}