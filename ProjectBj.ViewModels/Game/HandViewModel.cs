using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.ViewModels.Game
{
    class HandViewModel
    {
        public List<CardViewModel> CardList { get; set; }
        public int HandScore { get; set; }
    }
}