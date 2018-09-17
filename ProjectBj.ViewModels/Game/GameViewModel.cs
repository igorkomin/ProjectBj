using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class GameViewModel
    {
        public int SessionId { get; set; }
        public DealerPartial Dealer { get; set; }
        public PlayerPartial Player { get; set; }
        public List<PlayerPartial> Bots { get; set; }
    }
}