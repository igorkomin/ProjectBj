using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class Player: BaseEntity
    {
        public string Name { get; set; }
        public bool IsHuman { get; set; }
        public int Balance { get; set; }
        public bool InGame { get; set; }
    }
}