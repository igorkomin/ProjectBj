using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProjectBj.Service.Providers;
using ProjectBj.ViewModels;
using ProjectBj.ViewModels.Game;

namespace ProjectBj.Web.Controllers
{
    public class MainController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Start([FromBody]GameSettings settings)
        {
            
            return Ok(settings);
        }
        
        // POST: api/Main
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]GameSettings settings)
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
