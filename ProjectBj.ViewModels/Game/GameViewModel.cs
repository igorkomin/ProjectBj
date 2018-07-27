using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.ViewModels.Game
{
    public class GameViewModel
    {
        public int SessionId { get; set; }
        public DealerViewModel Dealer { get; set; }
        public PlayerViewModel Player { get; set; }
        public List<PlayerViewModel> Bots { get; set; }
    }
}