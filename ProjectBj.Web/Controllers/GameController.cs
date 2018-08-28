using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProjectBj.Logger;
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
                GameViewModel model = await _service.NewGame(settings.PlayerName, settings.BotsNumber);
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
        public async Task<IHttpActionResult> Logs([FromBody]IdentifierViewModel identifier)
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
    }
}