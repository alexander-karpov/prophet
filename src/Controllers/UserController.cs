using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prophet.Entities;
using Prophet.Exceptions;
using Prophet.Model;
using Prophet.Operations;

namespace Prophet.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("{userId}")]
        public ComebackResponse Get(string userId)
        {
            return new ComebackResponse
            {
                IsComeback = _comebackOperation.IsСomeback(userId)
            };
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] WelcomeRequest request)
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
