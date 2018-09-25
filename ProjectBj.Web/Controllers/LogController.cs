using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Logs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

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
                Log.Error(exception.Message);
                return InternalServerError(exception);
            }
        }
    }
}
