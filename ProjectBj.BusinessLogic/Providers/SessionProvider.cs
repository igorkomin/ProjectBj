using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DataAccess.Interfaces;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.BusinessLogic.Helpers;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Entities;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Providers
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IGameSessionRepository _sessionRepository;

        public SessionProvider(IGameSessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<SessionViewModel> CreateSession()
        {
            SessionViewModel sessionViewModel;
            GameSession session = new GameSession
            {
                TimeCreated = DateTime.Now
            };
            try
            {
                session = await _sessionRepository.Create(session);
                sessionViewModel = new SessionViewModel
                {
                    Id = session.Id,
                    IsOpen = session.IsOpen,
                    TimeCreated = session.TimeCreated
                };
                return sessionViewModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<SessionViewModel> GetSessionByPlayerId(int playerId)
        {
            GameSession currentSession = await _sessionRepository.GetCurrentSession(playerId);
            
            if (currentSession == null)
            {
                throw new Exception(StringHelper.NoGameToLoad);
            }
            
            SessionViewModel currentSessionViewModel = new SessionViewModel
            {
                Id = currentSession.Id,
                IsOpen = currentSession.IsOpen,
                TimeCreated = currentSession.TimeCreated
            };
 
            return currentSessionViewModel;
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