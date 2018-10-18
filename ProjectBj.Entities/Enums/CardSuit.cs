using System.ComponentModel;

namespace ProjectBj.Entities.Enums
{
    public enum CardSuit
    {
        None = 0,
        [Description("spades")]
        Spades = 1,
        [Description("clubs")]
        Clubs = 2,
        [Description("hearts")]
        Hearts = 3,
        [Description("diams")]
        Diamonds = 4
    }
}