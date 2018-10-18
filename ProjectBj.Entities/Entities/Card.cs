using System.ComponentModel.DataAnnotations.Schema;
using ProjectBj.Entities.Enums;

namespace ProjectBj.Entities
{
    [Table("Cards")]
    public class Card : BaseEntity
    {
        public CardSuits Suit { get; set; }
        public CardRanks Rank { get; set; }
    }
}