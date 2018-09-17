using System.Collections.Generic;

namespace ProjectBj.ViewModels.Game
{
    public class GameViewModel
    {
        public int SessionId { get; set; }
        public DealerInfo Dealer { get; set; }
        public PlayerInfo Player { get; set; }
        public List<PlayerInfo> Bots { get; set; }
    }
}