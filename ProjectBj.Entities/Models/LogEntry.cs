using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("Logs")]
    public class LogEntry : BaseEntity
    {
        public int SessionId { get; set; }
        public DateTime Time { get; set; }
        public string PlayerName { get; set; }
        public string Message { get; set; }
    }
}