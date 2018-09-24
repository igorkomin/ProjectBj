using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBj.Entities
{
    [Table("GameSessions")]
    public class GameSession : BaseEntity
    {
        public bool IsOpen { get; set; }
        public DateTime TimeCreated { get; set; }

        public GameSession()
        {
            IsOpen = true;
        }
    }
}