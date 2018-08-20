using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBj.Entities
{
    public class GameSession
    {
        public GameSession()
        {
            IsOpen = true;
        }

        [Key]
        public int Id { get; set; }

        public bool IsOpen { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}