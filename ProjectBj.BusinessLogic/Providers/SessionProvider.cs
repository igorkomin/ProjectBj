using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;
using System;
using System.Threading.Tasks;

namespace ProjectBj.BusinessLogic.Providers
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IGameSessionRepository _sessionRepository;

        public SessionProvider(IGameSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<GameSession> CreateSession()
        {
            GameSession session = new GameSession
            {
                TimeCreated = DateTime.Now
            };
            try
            {
                session = await _sessionRepository.Create(session);
                return session;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<GameSession> GetSessionByPlayerId(int playerId)
        {
            GameSession currentSession = await _sessionRepository.GetFirstUnfinishedSession(playerId);
            
            if (currentSession == null)
            {
                throw new Exception(StringHelper.NoGameToLoad);
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