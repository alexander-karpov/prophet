using Microsoft.AspNetCore.Mvc;
using Prophet.Dialog.Operations;

namespace Prophet.Dialog.Controllers
{
    [ApiController]
    [Route("{Controller}")]
    public class WebhookController : ControllerBase
    {
        private readonly DequeueOperation _dequeueOperation;

        public WebhookController(DequeueOperation dequeueOperation)
        {
            _dequeueOperation = dequeueOperation;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("200 KO");
        }

        [HttpPost]
        public ActionResult<string> Post()
        {
            return _dequeueOperation.Dequeue("E8FE0BFEF5EBC2FD5ECB7181A8FFC6E3465DA37B8A35DBF67A7D0455706A535C") switch
            {
                Just<string> article => article.Value,
                _ => "Сохраняйте спокойствие и ждите новостей"
            };
        }
    }
}
