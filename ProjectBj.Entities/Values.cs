using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.StringHelper;

namespace ProjectBj
{
    public class Values
    {
        public enum Suits
        {
            Clubs,
            Spades,
            Hearts,
            Diamonds
        }

        public enum Faces
        {
            Jack,
            Queen,
            King,
            Ace
        }

        public static Dictionary<string, int> cardValues = new Dictionary<string, int>()
        {
            [Strings.two] = 2,
            [Strings.three] = 3,
            [Strings.four] = 4,
            [Strings.five] = 5,
            [Strings.six] = 6,
            [Strings.seven] = 7,
            [Strings.eight] = 8,
            [Strings.nine] = 9,
            [Strings.ten] = 10,
            [Strings.jack] = 10,
            [Strings.queen] = 10,
            [Strings.king] = 10,
            [Strings.ace] = 11
        };
    }
}
