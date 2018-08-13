using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;
using ProjectBj.Logger;
using ProjectBj.Service.Interfaces;
using ProjectBj.Service.Providers;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Web.Controllers
{
    public class MainController : ApiController
    {
        private readonly IGameProvider _provider;

        public MainController(IGameProvider provider)
        {
            _provider = provider;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Game([FromBody]GameSettings settings)
        {
            GameViewModel model = await _provider.NewGame(settings.PlayerName, settings.BotsNumber);

            if(model == null)
            {
                return InternalServerError();
            }

            return Ok(model);
        }
    }
}