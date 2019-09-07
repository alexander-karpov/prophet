using Microsoft.AspNetCore.Mvc;

namespace Prophet.Dialog.Controllers
{
    [ApiController]
    [Route("{Controller}")]
    public class WebhookController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("200 KO");
        }

        [HttpPost]
        public ActionResult<string> Post()
        {
            return "OK";
        }
    }
}
