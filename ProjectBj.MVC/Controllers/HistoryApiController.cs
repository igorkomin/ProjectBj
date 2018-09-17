using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectBj.MVC.Controllers
{
    public class HistoryApiController : ApiController
    {
        private readonly IHistoryService _service;

        public HistoryApiController(IHistoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GameHistory([FromBody] int sessionId)
        {
            try
            {
                List<HistoryViewModel> history = await _service.GetSessionHistory(sessionId);
                return Ok(history);
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
                List<HistoryViewModel> logs = await _service.GetFullHistory();
                return Ok(logs);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}