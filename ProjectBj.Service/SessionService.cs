using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBj.DAL;
using ProjectBj.DAL.Repositories;
using ProjectBj.Entities;

namespace ProjectBj.Service
{
    public static class SessionService
    {
        private static GameSessionRepository _sessionRepository;

        static SessionService()
        {
            _sessionRepository = new GameSessionRepository();
        }

        public static GameSession CreateSession()
        {
            GameSession session = new GameSession { TimeCreated = DateTime.Now };
            try
            {
                session = _sessionRepository.Create(session);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return session;
        }

        public static void CloseSession(GameSession session)
        {
            session.IsOpen = false;
            try
            {
                _sessionRepository.Update(session);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void AddPlayer(GameSession session, Player player)
        {
            _sessionRepository.AddPlayer(session, player);
        }
    }
}
