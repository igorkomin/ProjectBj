using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;
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
        public async Task<IHttpActionResult> Start([FromBody]GameSettings settings)
        {
            try
            {
                GameViewModel model = await _service.GetNewGame(settings.PlayerName, settings.BotsNumber, settings.Bet);
                return Ok(model);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Load([FromBody]GameSettings settings)
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
        public async Task<IHttpActionResult> Hit([FromBody]IdentifierViewModel identifier)
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
        public async Task<IHttpActionResult> Stand([FromBody]IdentifierViewModel identifier)
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
        public async Task<IHttpActionResult> Double([FromBody]IdentifierViewModel identifier)
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
        public async Task<IHttpActionResult> Surrender([FromBody]IdentifierViewModel identifier)
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

        [HttpPost]
        public async Task<IHttpActionResult> History([FromBody]IdentifierViewModel identifier)
        {
            try
            {
                List<LogEntryViewModel> logs = await _service.GetSessionLogs(identifier.SessionId);
                return Ok(logs);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> FullHistory()
        {
            try
            {
                List<LogEntryViewModel> logs = await _service.GetAllLogs();
                return Ok(logs);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}