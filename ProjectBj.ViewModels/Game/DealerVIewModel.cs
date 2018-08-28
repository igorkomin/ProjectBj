using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.ViewModels.Game
{
    public class DealerViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public HandViewModel Hand { get; set; }
    }
}