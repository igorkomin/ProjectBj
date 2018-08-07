using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.ViewModels.Game
{
    public class HandViewModel
    {
        public List<CardViewModel> Cards { get; set; }
        public int Score { get; set; }
    }
}
