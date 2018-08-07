using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Service.Interfaces
{
    interface IPlayerService
    {
        Task<Player> GetDealer();
        Task<Player> GetPlayer(string name);
        Task<Player> GetPlayerById(int id);
        Task<PlayerViewModel> GetPlayerViewModel(string name);
        Task<DealerViewModel> GetDealerViewModel();
        Task<HandViewModel> GetHandViewModel(int playerId, int sessionId);
        Task<List<Card>> GetCards(int playerId, int sessionId);
        Task<List<CardViewModel>> GetCardViewModels(int playerId, int sessionId);
        Task<List<Player>> CreateBots(int number);
        Task<List<PlayerViewModel>> GetBotViewModelList(int botnumber, int sessionId);
        Task<int> GetHandValue(int playerId, int sessionId);
        Task DeletePlayer(Player player);
    }
}
