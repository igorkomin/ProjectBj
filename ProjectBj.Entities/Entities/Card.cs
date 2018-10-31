using System.ComponentModel.DataAnnotations.Schema;
using ProjectBj.Entities.Enums;

namespace ProjectBj.Entities
{
    [Table("Cards")]
    public class Card : BaseEntity
    {
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }
    }
}