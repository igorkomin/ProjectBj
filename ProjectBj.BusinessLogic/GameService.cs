﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DataAccess;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.Entities;
using ProjectBj.BusinessLogic.Enums;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;

namespace ProjectBj.BusinessLogic
{
    public class GameService : IGameService
    {
        private PlayerRepository _playerRepository;

        public GameService()
        {
            _playerRepository = new PlayerRepository();
        }

        public async Task<GameResults.Result> GetGameResult(int playerId, int playerScore, int dealerScore, int bet)
        {
            if (playerScore == ValueHelper.BlackjackValue)
            {
                int winAmount = (bet * 2) + (bet / 2);
                await ChangePlayerBalance(playerId, winAmount);
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
                return GameResults.Result.Won;
            }

            await ChangePlayerBalance(playerId, -bet);
            return GameResults.Result.Lost;
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