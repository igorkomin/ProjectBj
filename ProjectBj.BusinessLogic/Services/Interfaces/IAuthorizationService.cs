using ProjectBj.ViewModels.Authorization;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<ResponseLoginAuthorizationView> Login(string playerName);
    }
}
