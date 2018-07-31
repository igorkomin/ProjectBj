﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.ViewModels.Game
{
    public class DealerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool InGame { get; set; }
        public List<CardViewModel> Hand { get; set; }
    }
}