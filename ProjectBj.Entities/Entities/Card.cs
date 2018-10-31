using ProjectBj.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("Cards")]
    public class Card : BaseEntity
    {
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
    }
}