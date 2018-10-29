using ProjectBj.BusinessLogic.Managers.Interfaces;
using ProjectBj.DataAccess.Repositories.Interfaces;
using ProjectBj.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Managers
{
    public class GameSessionManager : IGameSessionManager
    {
        private readonly IGameSessionRepository _sessionRepository;

        public GameSessionManager(IGameSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<GameSession> GetNew()
        {
            var session = new GameSession();
            session = await _sessionRepository.Insert(session);
            return session;
        }

        public async Task<GameSession> GetByPlayerId(long playerId)
        {
            GameSession currentSession = await _sessionRepository.GetFirstOpen(playerId);
            
            if (currentSession == null)
            {
                throw new Exception(UserMessages.NoGameToLoadMessage);
            }
 
            return currentSession;
        }

        private async Task<GameSession> GetById(long id)
        {
            GameSession session = await _sessionRepository.GetById(id);
            return session;
        }

        public async Task Close(long sessionId)
        {
            GameSession session = await GetById(sessionId);
            session.IsOpen = false;
            await _sessionRepository.Update(session);
        }
    }
}