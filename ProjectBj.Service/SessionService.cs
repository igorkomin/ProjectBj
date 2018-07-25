﻿using System;
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

        public SessionService()
        {
            _sessionRepository = new GameSessionRepository();
        }

        public GameSession CreateSession()
        {
            GameSession session = new GameSession { TimeCreated = DateTime.Now };
            try
            {
                session = _sessionRepository.Create(session);
                return session;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void CloseSession(GameSession session)
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

        public void AddPlayer(GameSession session, Player player)
        {
            _sessionRepository.AddPlayer(session, player);
        }
    }
}
