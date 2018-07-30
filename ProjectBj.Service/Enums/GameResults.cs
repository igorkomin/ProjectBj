﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Service.Enums
{
    public class GameResults
    {
        public enum Result
        {
            Unfinished = 0,
            Blackjack = 1,
            Bust = 2,
            Win = 3,
            Lose = 4,
        }
    }
}
