namespace ProjectBj.BusinessLogic.Helpers.Interfaces
{
    public interface IGameResultHelper
    {
        (int gameState, string gameResult) GetGameStateResult(int playerScore, int dealerScore);
    }
}