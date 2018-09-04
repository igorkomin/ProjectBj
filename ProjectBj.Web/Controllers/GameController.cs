using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Web.Controllers
{
    public class GameController : ApiController
    {
        private readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Start([FromBody]GameSettings settings)
        {
            try
            {
                GameViewModel model = await _service.NewGame(settings.PlayerName, settings.BotsNumber, settings.Bet);
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
                GameViewModel model = await _service.LoadGame(settings.PlayerName);
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
                GameViewModel model = await _service.Hit(identifier.PlayerId, identifier.SessionId);
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
                GameViewModel model = await _service.Stand(identifier.PlayerId, identifier.SessionId);
                return Ok(model);
            }
            catch(Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Double([FromBody]IdentifierViewModel identifier)
        {
            try
            {
                GameViewModel model = await _service.DoubleDown(identifier.PlayerId, identifier.SessionId);
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
                GameViewModel model = await _service.Surrender(identifier.PlayerId, identifier.SessionId);
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
                List<LogEntryViewModel> logs = await _service.GetLogs(identifier.SessionId);
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