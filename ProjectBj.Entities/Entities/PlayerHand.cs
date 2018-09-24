using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("PlayerHands")]
    public class PlayerHand : BaseEntity
    {
        public int PlayerId { get; set; }
        public int CardId { get; set; }
        public int SessionId { get; set; }
    }
}