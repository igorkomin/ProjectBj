using ProjectBj.BusinessLogic.Helpers.Interfaces;
using ProjectBj.Entities.Enums;

namespace ProjectBj.BusinessLogic.Helpers
{
    public class GameResultHelper : IGameResultHelper
    {
        public (int gameState, string gameResult) GetGameStateResult(int playerScore, int dealerScore)
        {
            var gameState = (int)GetGameResult(playerScore, dealerScore);
            string gameResult = GetGameResult(playerScore, dealerScore).ToString();
            return (gameState, gameResult);
        }

        private GameResult GetGameResult(int playerScore, int dealerScore)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                return GameResult.Blackjack;
            }
            if (playerScore > ValueHelper.BlackjackValue)
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
            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                return GameResult.Win;
            }

            return GameResult.Lose;
        }

    }
}