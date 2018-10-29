using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.Enums;

namespace ProjectBj.BusinessLogic.Managers
{
    internal class GameResultManager : IGameResultManager
    {
        public (int gameState, string gameResult) GetGameStateResult(int playerScore, int dealerScore)
        {
            var gameState = (int)GetGameResult(playerScore, dealerScore);
            string gameResult = GetGameResult(playerScore, dealerScore).ToString();
            return (gameState, gameResult);
        }

        private GameResult GetGameResult(int playerScore, int dealerScore)
        {
            if (playerScore == Constants.BlackjackValue)
            {
                return GameResult.Blackjack;
            }
            if (playerScore > Constants.BlackjackValue)
            {
                return GameResult.Bust;
            }
            if (playerScore == dealerScore)
            {
                return GameResult.Win;
            }
            if (playerScore == 0)
            {
                return GameResult.Surrender;
            }
            if (playerScore > dealerScore || dealerScore > Constants.BlackjackValue)
            {
                return GameResult.Win;
            }

            return GameResult.Lose;
        }

    }
}