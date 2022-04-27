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
        [HttpPost("/CreateLike")]
        public IActionResult CreateLike([FromBody] ModelClass.CreateLike input)
        {
            var result = Repository.LikeRepository.CreateLikeFromIdAccountIdPost(input);
            return result == null ? Conflict() : Ok(result);
        }

        [HttpGet("/GetLikesFromIdPost")]
        public IActionResult GetLikesFromIdPost(long id)
        {
            var result = Repository.LikeRepository.GetLikesFromIdPost(id);
            return result == null ? Conflict() : Ok(result);
        }

        [HttpGet("/GetLikeFromId")]
        public IActionResult GetLikeFromId(long id)
        {
            var result = Repository.LikeRepository.GetLikeFromId(id);
            return result == null ? Conflict() : Ok(result);
        }
        
        [Authorize]
        [HttpGet("/GetLikesAccount")]
        public IActionResult GetLikesAccount()
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();            

            var result = Repository.LikeRepository.GetLikesFromIdAccount((long)id);
            return result == null ? Conflict() : Ok(result);
        }

        [HttpGet("/GetLikesFromIdAccount")]
        public IActionResult GetLikesFromIdAccount(long id)
        {
            var result = Repository.LikeRepository.GetLikesFromIdAccount(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpDelete("/DeleteLikeFromId")]
        public IActionResult DeleteLikeFromId(long id)
        {
            var result = Repository.LikeRepository.DeleteLikeFromId(id);
            return result == false ? Conflict() : Ok();
        }
    }
}