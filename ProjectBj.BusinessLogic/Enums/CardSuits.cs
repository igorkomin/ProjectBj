using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Enums
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
