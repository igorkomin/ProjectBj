using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("PlayerHands")]
    public class PlayerHand : BaseEntity
    {
        public long PlayerId { get; set; }
        public long CardId { get; set; }
        public long SessionId { get; set; }
    }
}