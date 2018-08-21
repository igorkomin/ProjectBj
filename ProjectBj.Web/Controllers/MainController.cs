﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Cors;
using ProjectBj.Logger;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.BusinessLogic.Providers;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Web.Controllers
{
    public class MainController : ApiController
    {
        private readonly IGameService _service;

        public MainController(IGameService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Game([FromBody]GameSettings settings)
        {
            try
            {
                GameViewModel model = await _service.NewGame(settings.PlayerName, settings.BotsNumber);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Hit(int playerId, int sessionId)
        {
            try
            {
                GameViewModel model = await _service.Hit(playerId, sessionId);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Stand(int playerId, int sessionId)
        {
            try
            {
                GameViewModel model = await _service.Stand(playerId, sessionId);
                return Ok(model);
            }
            catch(Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}