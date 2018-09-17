﻿using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;
using ProjectBj.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectBj.MVC.Controllers
{
    public class GameApiController : ApiController
    {
        private readonly IGameService _service;

        public GameApiController(IGameService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Start([FromBody]Settings settings)
        {
            try
            {
                GameViewModel model = await _service.GetNewGame(settings.PlayerName, settings.BotsNumber);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Load([FromBody]Settings settings)
        {
            try
            {
                GameViewModel model = await _service.GetUnfinishedGame(settings.PlayerName);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Hit([FromBody]Identifier identifier)
        {
            try
            {
                GameViewModel model = await _service.MakeHitDecision(identifier.PlayerId, identifier.SessionId);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Stand([FromBody]Identifier identifier)
        {
            try
            {
                GameViewModel model = await _service.MakeStandDecision(identifier.PlayerId, identifier.SessionId);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Double([FromBody]Identifier identifier)
        {
            try
            {
                GameViewModel model = await _service.MakeDoubleDownDecision(identifier.PlayerId, identifier.SessionId);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Surrender([FromBody]Identifier identifier)
        {
            try
            {
                GameViewModel model = await _service.MakeSurrenderDecision(identifier.PlayerId, identifier.SessionId);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}