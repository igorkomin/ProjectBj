using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Providers
{
    public class GameSessionProvider : IGameSessionProvider
    {
        private readonly IGameSessionRepository _sessionRepository;

        public GameSessionProvider(IGameSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<GameSession> GetNew()
        {
            var session = new GameSession
            {
                CreationDate = DateTime.Now
            };
            session = await _sessionRepository.Insert(session);
            return session;
        }

        public async Task<GameSession> GetByPlayerId(long playerId)
        {
            GameSession currentSession = await _sessionRepository.GetFirstOpen(playerId);
            
            if (currentSession == null)
            {
                throw new Exception(StringHelper.NoGameToLoadMessage);
            }
 
            return currentSession;
        }

        public async Task<GameSession> GetById(long id)
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