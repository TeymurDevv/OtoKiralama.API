using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OtoKiralama.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register()
        {
            return Ok();
        }
    }
}
