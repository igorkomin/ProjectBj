using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.ViewModels
{
    public class PlayerView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public int Balance { get; set; }
        public bool InGame { get; set; }
        public List<Card> Cards { get; set; }
    }
}
