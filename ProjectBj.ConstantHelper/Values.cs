using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.StringHelper;

namespace ProjectBj.ConstantHelper
{
    public static class Values
    {
        public static int blackjackValue = 21;
        public static int aceDelta = 10;
        public static int minDealerHandValue = 17;

        public static string[] suits = { };

        public static Dictionary<int, int> cardValues = new Dictionary<int, int>()
        {
            [0] = 2,
            [1] = 3,
            [2] = 4,
            [3] = 5,
            [4] = 6,
            [5] = 7,
            [6] = 8,
            [7] = 9,
            [8] = 10,
            [9] = 10,
            [10] = 10,
            [11] = 10,
            [12] = 11
        };
    }
}
