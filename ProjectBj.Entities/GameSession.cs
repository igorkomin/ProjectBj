using System;

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