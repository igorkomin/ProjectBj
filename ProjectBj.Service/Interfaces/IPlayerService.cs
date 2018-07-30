using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;

namespace ProjectBj.Service.Interfaces
{
    interface IPlayerService
    {
        Task<Player> GetDealer();
        Task<Player> GetPlayer(string name);
        Task<Player> GetPlayerById(int id);
        Task<List<Player>> CreateBots(int number);
        Task<List<Card>> GetCards(int playerId, int sessionId);
        Task<int> GetHandValue(int playerId, int sessionId);
        Task ThrowCards(Player player);
        Task DeletePlayer(Player player);
    }
}
