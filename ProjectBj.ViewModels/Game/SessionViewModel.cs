using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.ViewModels.Game
{
    public class SessionViewModel : BaseViewModel
    {
        public bool IsOpen { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
