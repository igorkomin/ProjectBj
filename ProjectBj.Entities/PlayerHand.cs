namespace ProjectBj.Entities
{
    public class PlayerHand : BaseEntity
    {
        public int PlayerId { get; set; }
        public int CardId { get; set; }
        public int SessionId { get; set; }
    }
}