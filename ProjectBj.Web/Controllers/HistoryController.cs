using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectBj.Web.Controllers
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _service;

        public HistoryController(IHistoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Game([FromBody] int sessionId)
        {
            try
            {
                List<GameHistoryView> view = await _service.GetSessionHistory(sessionId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> Full()
        {
            try
            {
                List<FullHistoryView> view = await _service.GetFullHistory();
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
