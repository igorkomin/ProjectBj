﻿using System.Collections.Generic;

namespace ProjectBj.Entities
{
    public class Game
    {
        public Player Dealer { get; set; }
        public Player Player { get; set; }
        public List<Player> Bots { get; set; }
    }
}
