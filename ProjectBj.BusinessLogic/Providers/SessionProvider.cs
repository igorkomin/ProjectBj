using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DataAccess;
using ProjectBj.DataAccess.Repositories;
using ProjectBj.Entities;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.BusinessLogic.Providers
{
    public class SessionProvider : ISessionProvider
    {
        private GameSessionRepository _sessionRepository;
        private PlayerRepository _playerRepository;

        public SessionProvider()
        {
            _sessionRepository = new GameSessionRepository();
            _playerRepository = new PlayerRepository();
        }

        private async Task<SessionViewModel> CreateSession()
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
            Player player = await _playerRepository.Get(playerId);
            GameSession currentSession = await _sessionRepository.GetCurrentSession(player);
            SessionViewModel currentSessionViewModel = new SessionViewModel
            {
                Id = currentSession.Id,
                IsOpen = currentSession.IsOpen,
                TimeCreated = currentSession.TimeCreated
            };

            if(currentSession == null)
            {
                SessionViewModel newSession = await CreateSession();
                return newSession;
            }

            return currentSessionViewModel;
        }

        public async Task<GameSession> GetSessionById(int id)
        {
            GameSession session = await _sessionRepository.Get(id);
            return session;
        }

        public async Task CloseSession(int sessionId)
        {
            GameSession session = await GetSessionById(sessionId);
            session.IsOpen = false;
            try
            {
                await _sessionRepository.Update(session.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}