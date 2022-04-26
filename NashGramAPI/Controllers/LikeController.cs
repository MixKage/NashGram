using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NashGramAPI.Model;
using NashGramAPI.Repository;

using static NashGramAPI.Model.ModelClass;

namespace NashGramAPI.Controllers
{    
    [Route("Like")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        [Authorize]
        [HttpGet("/GetLikesFromIdPost")]
        public IActionResult GetLikeFromIdPost(long id)
        {
            var result = Repository.LikeRepository.GetLikesFromIdPost(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetLikeFromId")]
        public IActionResult GetLikeFromId(long id)
        {
            var result = Repository.LikeRepository.GetLikeFromId(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetLikeFromIdAccount")]
        public IActionResult GetLikeFromIdAccount(long id)
        {
            var result = Repository.LikeRepository.GetLikeFromIdAccount(id);
            return result == null ? Conflict() : Ok(result);
        }
    }
}