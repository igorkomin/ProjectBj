using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Configuration
{
    public static class Values
    {
        public static int blackjackValue = 21;
        public static int aceCardIndex = 12;
        public static int aceDelta = 10;
        public static int minDealerHandValue = 17;
        public static int startBalance = 1000;

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
