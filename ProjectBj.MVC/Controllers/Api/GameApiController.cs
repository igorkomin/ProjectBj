using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Game;
using System;
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
        public async Task<IHttpActionResult> Start([FromBody]RequestStartGameView request)
        {
            try
            {
                ResponseStartGameView view = await _service.GetNewGame(request.PlayerName, request.BotsNumber);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Load([FromBody]RequestLoadGameView request)
        {
            try
            {
                ResponseLoadGameView view = await _service.GetUnfinishedGame(request.PlayerName);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Hit([FromBody]RequestHitGameView request)
        {
            try
            {
                ResponseHitGameView view = await _service.MakeHitDecision(request.PlayerId, request.SessionId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Stand([FromBody]RequestStandGameView request)
        {
            try
            {
                ResponseStandGameView view = await _service.MakeStandDecision(request.PlayerId, request.SessionId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Double([FromBody]RequestDoubleGameView request)
        {
            try
            {
                ResponseDoubleGameView view = await _service.MakeDoubleDownDecision(request.PlayerId, request.SessionId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Surrender([FromBody]RequestSurrenderGameView request)
        {
            try
            {
                ResponseSurrenderGameView view = await _service.MakeSurrenderDecision(request.PlayerId, request.SessionId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }
    }
}