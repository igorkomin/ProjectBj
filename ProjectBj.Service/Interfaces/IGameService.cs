using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.Service.Enums;

namespace ProjectBj.Service.Interfaces
{
    interface IGameService
    {
        Task<GameResults.Result> GetGameResult(int playerId, int playerScore, int dealerScore, int bet);
        Task ChangePlayerBalance(int playerId, int balanceDelta);
    }
}
