using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("Cards")]
    public class Card : BaseEntity
    {
        public string Suit { get; set; }
        public int Rank { get; set; }
    }
}