using ProjectBj.BusinessLogic.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Logs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectBj.Web.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class LogController : ApiController
    {
        private readonly ISystemLogService _service;

        public LogController(ISystemLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetFullLog()
        {
            try
            {
                List<GetFullLogView> view = await _service.GetFullLog();
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
