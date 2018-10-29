using ProjectBj.ViewModels.Authorization;
using System.Threading.Tasks;
using System.Web.Http;
using ProjectBj.BusinessLogic.Services.Interfaces;

namespace ProjectBj.Web.Controllers.Api
{
    public class AuthorizationController : ApiController
    {
        private readonly IAuthorizationService _service;

        public AuthorizationController(IAuthorizationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login(RequestLoginAuthorizationView request)
        {
            ResponseLoginAuthorizationView response = await _service.Login(request.PlayerName);
            return Ok(response);
        }
    }
}
