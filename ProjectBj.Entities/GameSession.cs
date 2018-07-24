using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class GameSession
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; } = true;
        public DateTime TimeCreated { get; set; }
    }
}
