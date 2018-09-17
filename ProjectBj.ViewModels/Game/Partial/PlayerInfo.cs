namespace ProjectBj.ViewModels.Game
{
    public class PlayerInfo : DealerInfo
    {
        public bool IsHuman { get; set; }
        //public int Balance { get; set; }
        public int GameResult { get; set; }
        public string GameResultMessage { get; set; }
        //public int Bet { get; set; }
        //public int BalanceDelta { get; set; }
    }
}