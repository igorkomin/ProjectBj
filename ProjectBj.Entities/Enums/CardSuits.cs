using System.ComponentModel;

namespace ProjectBj.Entities.Enums
{
    public class CardSuits
    {
        public enum Suit
        {
            [Description("spades")]
            Spades = 0,
            [Description("clubs")]
            Clubs = 1,
            [Description("hearts")]
            Hearts = 2,
            [Description("diams")]
            Diamonds = 3
        }
    }
}
