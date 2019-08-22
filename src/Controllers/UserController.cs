using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prophet.Entities;
using Prophet.Exceptions;
using Prophet.Model;
using Prophet.Operations;

namespace Prophet.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        readonly WelcomeOperation _welcomeOperation;
        readonly СomebackOperation _comebackOperation;

        public UserController(
            WelcomeOperation welcomeOperation,
            СomebackOperation comebackOperation
        )
        {
            _comebackOperation = comebackOperation;
            _welcomeOperation = welcomeOperation;
        }

        [HttpGet("comeback/{userId}")]
        public ActionResult<ComebackResponse> Comeback(string userId)
        {
            if (!_comebackOperation.IsСomeback(userId))
            {
                return NotFound($"Пользователь {userId} не найден");
            }

            return new ComebackResponse
            {
                Article = "Любопытной варваре на базаре нос оторвали"
            };
        }

        [HttpPost("welcome")]
        public ActionResult<string> Welcome([FromBody] WelcomeRequest request)
        {
            try
            {
                _welcomeOperation.Welcome(request.UserId);

                return "Пользователь добавлен";
            }
            catch (AlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
