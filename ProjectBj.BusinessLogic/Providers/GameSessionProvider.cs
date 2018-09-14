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

        public async Task<GameSession> GetNewSession()
        {
            GameSession session = new GameSession
            {
                TimeCreated = DateTime.Now
            };
            session = await _sessionRepository.Insert(session);
            return session;
        }

        public async Task<GameSession> GetSessionByPlayerId(int playerId)
        {
            GameSession currentSession = await _sessionRepository.GetFirstOpen(playerId);
            
            if (currentSession == null)
            {
                throw new Exception(StringHelper.NoGameToLoadMessage);
            }
 
            return currentSession;
        }

        public async Task<GameSession> GetSessionById(int id)
        {
            GameSession session = await _sessionRepository.GetById(id);
            return session;
        }

        public async Task CloseSession(int sessionId)
        {
            GameSession session = await GetSessionById(sessionId);
            session.IsOpen = false;
            await _sessionRepository.Update(session);
        }
    }
}