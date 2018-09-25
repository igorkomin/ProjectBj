using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("History")]
    public class History : BaseEntity
    {
        public long SessionId { get; set; }
        public string PlayerName { get; set; }
        public string Event { get; set; }
    }
}