using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.ViewModels.Logs;

namespace ProjectBj.Web.Controllers
{
    public class LogController : ApiController
    {
        private readonly ISystemLogService _service;

        public LogController(ISystemLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Full()
        {
            try
            {
                List<FullLogView> view = await _service.GetSystemLogs();
                return Ok(view);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
    }
}
