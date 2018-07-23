using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    class GameSession
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public DateTime TimeJoined { get; set; }
        public int Score { get; set; }
    }
}
