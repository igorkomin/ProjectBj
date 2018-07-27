using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.ViewModels.Game
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string Suit { get; set; }
        public string Rank { get; set; }
        public string ImageUrl { get; set; }
    }
}