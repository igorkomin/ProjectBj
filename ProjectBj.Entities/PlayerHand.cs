using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class PlayerHand : BaseEntity
    {
        public int PlayerId { get; set; }
        public int CardId { get; set; }
        public int SessionId { get; set; }
    }
}