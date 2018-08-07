using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Http;
using ProjectBj.Logger;
using ProjectBj.Service.Providers;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Web.Controllers
{
    public class MainController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Game([FromBody]GameSettings settings)
        {
            GameProvider provider = new GameProvider(settings.PlayerName, settings.BotsNumber);
            GameViewModel model = await provider.NewGame();

            if(model == null)
            {
                return InternalServerError();
            }

            return Ok(model);
        }
    }
}
