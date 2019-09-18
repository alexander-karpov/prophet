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
        private readonly SubscribeOperation _subscribeOperation;

        public WebhookController(DequeueOperation dequeueOperation, SubscribeOperation subscribeOperation)
        {
            _dequeueOperation = dequeueOperation;
            _subscribeOperation = subscribeOperation;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("200 OK");
        }

        [HttpPost]
        public ActionResult<DialogsResponse> Post(DialogsRequest req)
        {
            _subscribeOperation.Subscribe(req.Session.UserId, "41946361"); // Дмитрий Емец
            _subscribeOperation.Subscribe(req.Session.UserId, "2222944"); // Андрей Ромашко
            _subscribeOperation.Subscribe(req.Session.UserId, "19458733"); // Степан Берёзкин
            _subscribeOperation.Subscribe(req.Session.UserId, "1152487"); // Владимир Киняйкин

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
