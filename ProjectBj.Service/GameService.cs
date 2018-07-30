using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.Service.Enums;
using ProjectBj.Service.Helpers;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service
{
    public class GameService : IGameService
    {
        private PlayerRepository _playerRepository;

        public async Task<GameResults.Result> GetGameResult(int playerId, int playerScore, int dealerScore, int bet)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                await ChangePlayerBalance(playerId, bet);
                return GameResults.Result.Blackjack;
            }

            if (playerScore > ValueHelper.BlackjackValue)
            {
                await ChangePlayerBalance(playerId, -bet);
                return GameResults.Result.Bust;
            }

            if (playerScore > dealerScore || dealerScore > ValueHelper.BlackjackValue)
            {
                await ChangePlayerBalance(playerId, bet);
                return GameResults.Result.Win;
            }

            await ChangePlayerBalance(playerId, -bet);
            return GameResults.Result.Lose;
        }

        public async Task ChangePlayerBalance(int playerId, int balanceDelta)
        {
            Player player = await _playerRepository.Get(playerId);
            player.Balance += balanceDelta;
            try
            {
                await _playerRepository.Update(player);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
