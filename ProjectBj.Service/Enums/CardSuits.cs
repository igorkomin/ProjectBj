using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Service.Enums
{
    public class CardSuitEnum
    {
        public enum Suit
        {
            [Description("&spades;")]
            Spades = 1,
            [Description("&clubs;")]
            Clubs = 2,
            [Description("&hearts;")]
            Hearts = 3,
            [Description("&diams;")]
            Diamonds = 4
        }
    }
}
