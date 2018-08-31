using ProjectBj.ViewModels.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Interfaces
{
    public interface IPlayerProvider
    {
        Task<DealerViewModel> GetDealer();
        Task<PlayerViewModel> GetPlayerViewModel(string name);
        Task<PlayerViewModel> GetPlayerById(int id);
        Task<PlayerViewModel> PullPlayer(string name);
        Task<HandViewModel> GetHandViewModel(int playerId, int sessionId);
        Task<List<PlayerViewModel>> GetBotViewModels(int botnumber, int sessionId);
        Task<List<PlayerViewModel>> GetSessionBotViewModels(int sessionId);
        Task GivePlayerCard(int playerId, int sessionId, int cardId);
        Task<int> GetHandValue(int playerId, int sessionId);
        Task ChangePlayerBalance(int playerId, int balanceDelta);
    }
}
