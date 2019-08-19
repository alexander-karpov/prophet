// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Prophet.Entities;
// using Prophet.Model;
// using Prophet.Operations;

// namespace Prophet.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class FeedController : ControllerBase
//     {
//         readonly AddFeedOperation _addFeedOperation;

//         public FeedController(AddFeedOperation addFeedOperation)
//         {
//             _addFeedOperation = addFeedOperation;
//         }

//         [HttpPost]
//         public async Task<string> Post([FromBody] AddFeedRequest request)
//         {
//             await _addFeedOperation.AddFeed(request.userId, request.feedUrl);

//             return "Источник новостей добавлен";
//         }
//     }
// }
