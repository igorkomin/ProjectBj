using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.Logs;
using System;
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
        public async Task<IHttpActionResult> GetFullLog()
        {
            try
            {
                GetFullLogView view = await _service.GetFullLog();
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                return InternalServerError(exception);
            }
        }
    }
}
