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

        private GameResults GetGameResult(int playerScore, int dealerScore)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                return GameResults.Blackjack;
            }
            if (playerScore > ValueHelper.BlackjackValue)
            {
                return GameResults.Bust;
            }
            if (playerScore == dealerScore)
            {
                return GameResults.Win;
            }
            if (playerScore == 0)
            {
                return GameResults.Surrender;
            }
            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                return GameResults.Win;
            }

            return GameResults.Lose;
        }

    }
}