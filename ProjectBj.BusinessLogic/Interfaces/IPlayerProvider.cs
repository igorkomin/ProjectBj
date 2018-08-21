using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IPlayerProvider
    {
        Task<DealerViewModel> GetDealer();
        Task<PlayerViewModel> GetPlayerViewModel(string name);
        Task<Player> GetPlayerById(int id);
        Task<HandViewModel> GetHandViewModel(int playerId, int sessionId);
        Task<List<PlayerViewModel>> GetBotViewModels(int botnumber, int sessionId);
        Task<int> GetHandValue(int playerId, int sessionId);
        Task DeletePlayer(Player player);
    }
}
