using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;
using ProjectBj.Service.Interfaces;

namespace ProjectBj.Service
{
    public class SessionService : ISessionService
    {
        private GameSessionRepository _sessionRepository;
        private PlayerRepository _playerRepository;

        public SessionService()
        {
            _sessionRepository = new GameSessionRepository();
            _playerRepository = new PlayerRepository();
        }

        private async Task<int> CreateSession()
        {
            GameSession session = new GameSession { TimeCreated = DateTime.Now };
            try
            {
                session = await _sessionRepository.Create(session);
                return session.Id;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<int> GetSession(int playerId)
        {
            Player player = await _playerRepository.Get(playerId);
            GameSession currentSession = await _sessionRepository.GetCurrentSession(player);

            if(currentSession == null)
            {
                int newSessionId = await CreateSession();
                return newSessionId;
            }

            return currentSession.Id;
        }

        public async Task CloseSession(GameSession session)
        {
            session.IsOpen = false;
            try
            {
                await _sessionRepository.Update(session);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
