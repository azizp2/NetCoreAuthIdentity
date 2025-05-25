using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet] // atau [HttpPost], tergantung kebutuhan
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(detail: context?.Error?.Message);
        }
    }

}
