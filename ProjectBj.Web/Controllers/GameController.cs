using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels.Game;
using System;
using System.Threading.Tasks;
using System.Web.Http;

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
                GameViewModel model = await _service.GetNewGame(settings.PlayerName, settings.BotsNumber);
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
        public async Task<IHttpActionResult> Hit([FromBody]GameIdentifier identifier)
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
        public async Task<IHttpActionResult> Stand([FromBody]GameIdentifier identifier)
        {
            try
            {
                GameViewModel model = await _service.MakeStandDecision(identifier.PlayerId, identifier.SessionId);
                return Ok(model);
            }
            catch(Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Double([FromBody]GameIdentifier identifier)
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
        public async Task<IHttpActionResult> Surrender([FromBody]GameIdentifier identifier)
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