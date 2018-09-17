namespace ProjectBj.ViewModels.Game
{
    public class PlayerInfo : DealerInfo
    {
        public bool IsHuman { get; set; }
        public int GameResult { get; set; }
        public string GameResultMessage { get; set; }
    }
}