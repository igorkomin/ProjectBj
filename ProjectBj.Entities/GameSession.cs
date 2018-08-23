using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class GameSession : BaseEntity
    {
        public GameSession()
        {
            IsOpen = true;
        }
        public bool IsOpen { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}