using System;
using Microsoft.AspNetCore.Mvc;
using Prophet.Dialog.Operations;

namespace Prophet.Dialog.Controllers
{
    [ApiController]
    [Route("{Controller}")]
    public class WebhookController : ControllerBase
    {
        const int MAX_ANSWER_LENGTH = 1024;

        private readonly DequeueOperation _dequeueOperation;

        public WebhookController(DequeueOperation dequeueOperation)
        {
            _dequeueOperation = dequeueOperation;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("200 OK");
        }

        [HttpPost]
        public ActionResult<DialogsResponse> Post(DialogsRequest req)
        {
            var message = _dequeueOperation.Dequeue(req.Session.UserId) switch
            {
                Just<string> article => article.Value,
                _ => "Сохраняйте спокойствие и ждите новостей"
            };

            return BuildDialogsResponse(req, message);
        }

        private DialogsResponse BuildDialogsResponse(DialogsRequest req, string text)
        {
            var session = req.Session;
            var cuttedText = text.Substring(0, Math.Min(text.Length, MAX_ANSWER_LENGTH));

            return new DialogsResponse
            {
                Response = new WebhookResponseData
                {
                    Text = cuttedText
                },
                Session = new WebhookResponseSession
                {
                    MessageId = session.MessageId,
                    SessionId = session.SessionId,
                    UserId = session.UserId
                },
                Version = req.Version
            };
        }
    }
}
