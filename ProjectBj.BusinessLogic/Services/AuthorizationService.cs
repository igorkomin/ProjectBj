using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.Entities;
using ProjectBj.Entities.Enums;
using ProjectBj.ViewModels.Authorization;
using System;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPlayerManager _playerManager;

        public AuthorizationService(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public async Task<ResponseLoginAuthorizationView> Login(string playerName)
        {
            if (playerName == PlayerType.Dealer.ToString())
            {
                throw new ArgumentException(UserMessages.NameReservedMessage);
            }

            Player player = await _playerManager.GetPlayerByName(playerName);
            var view = new ResponseLoginAuthorizationView
            {
                PlayerId = player.Id
            };
            return view;
        }
    }
}
