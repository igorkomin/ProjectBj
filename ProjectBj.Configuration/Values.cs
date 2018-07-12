using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Configuration
{
    public static class Values
    {
        public static readonly int blackjackValue = 21;
        public static readonly int aceCardIndex = 12;
        public static readonly int aceDelta = 10;
        public static readonly int minDealerHandValue = 17;
        public static readonly int startBalance = 1000;

        public static Dictionary<int, int> cardValues = new Dictionary<int, int>()
        {
            [2] = 2,
            [3] = 3,
            [4] = 4,
            [5] = 5,
            [6] = 6,
            [7] = 7,
            [8] = 8,
            [9] = 9,
            [10] = 10,
            [11] = 10,
            [12] = 10,
            [13] = 10,
            [14] = 11
        };
    }
}
