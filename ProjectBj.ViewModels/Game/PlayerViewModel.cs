﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.ViewModels.Game
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public bool InGame { get; set; }
        public int Balance { get; set; }
        public int GameResult { get; set; }
        public int Bet { get; set; }
        public HandViewModel Hand { get; set; }
    }
}