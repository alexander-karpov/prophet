using System;
using Microsoft.AspNetCore.Mvc;
using Prophet.Dto;
using Prophet.Exceptions;
using Prophet.Operations;

namespace Prophet.Controllers
{
    [ApiController]
    [Route("{Controller}")]
    public class WebhookController : ControllerBase
    {
        readonly StartOperation _startOperation;

        public WebhookController(StartOperation startOperation)
        {
            _startOperation = startOperation;
        }
        public ActionResult<StartResponse> Post(WebhookRequest request)
        {
            var result = _startOperation.Start(request.UserId);

            return _startOperation.Start(request.UserId) switch
            {
                Newcomer _ => new StartResponse { IsNew = true },
                UserWaitArticle _ => new StartResponse { IsNew = false },
                UserHasArticle u => new StartResponse
                {
                    IsNew = false,
                    ArticleAuthor = "Дмитрий Емец",
                    ArticleText = u.Article.Text
                },
                _ => throw new UnexpectedResult(nameof(StartOperation))
            };
        }
    }
}
