using System;

namespace ProjectBj.ViewModels.Game
{
    public class SessionViewModel : BaseViewModel
    {
        public bool IsOpen { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
