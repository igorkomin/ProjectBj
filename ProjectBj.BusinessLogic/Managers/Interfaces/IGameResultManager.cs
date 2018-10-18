namespace ProjectBj.BusinessLogic.Managers.Interfaces
{
    public interface IGameResultManager
    {
        (int gameState, string gameResult) GetGameStateResult(int playerScore, int dealerScore);
    }
}