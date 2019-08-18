using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prophet.Entities;
using Prophet.Operations;

namespace Prophet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        readonly PullArticleOperation _pullArticleOperation;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(PullArticleOperation pullArticleOperation)
        {
            _pullArticleOperation = pullArticleOperation;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Article>> Get(string userId)
        {
            if (await _pullArticleOperation.Pull(userId) is Just<Article> a)
            {
                return a.Value;
            }

            return NotFound();
        }
    }
}
