using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NashGramAPI.Model;
using NashGramAPI.Repository;

using static NashGramAPI.Model.ModelClass;

namespace NashGramAPI.Controllers
{
    [Route("Post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [Authorize]
        [HttpPost("/UpdateImageFromIdPost")]
        public IActionResult UpdateImageFromIdPost([FromBody] ModelClass.UpdateInput updateInput)
        {
            var result = Repository.PostRepository.UpdateInfoFromIdPost(updateInput, 0);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPost("/CreatePost")]
        public IActionResult CreatePost(ModelClass.PostCreate input)
        {
            var result = Repository.PostRepository.CreatePost(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetAuthorFromId")]
        public IActionResult GetAuthor(long input)
        {
            var result = Repository.PostRepository.GetAuthor(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetUriFromId")]
        public IActionResult GetUri(long input)
        {
            var result = Repository.PostRepository.GetUri(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetDescryptionFromId")]
        public IActionResult GetDescryption(long input)
        {
            var result = Repository.PostRepository.GetDescryption(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetTagFromId")]
        public IActionResult GetTag(long id)
        {
            var result = Repository.PostRepository.GetTag(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetPostFromIdPost")]
        public IActionResult GetPostFromIdPost(long id)
        {
            var result = Repository.PostRepository.GetPostFromIdPost(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetPostsFromIdAccount")]
        public IActionResult GetPostsFromIdAccount(long id)
        {
            var result = Repository.PostRepository.GetPostsFromIdAccount(id);
            return result == null ? Conflict() : Ok(result);
        }
        
        [HttpGet("/GetAllPosts")]
        public IActionResult GetAllPosts()
        {
            var result = Repository.PostRepository.GetAllPosts();
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpDelete("/DeletePostsFromIdPost")]
        public IActionResult DeletePostsFromIdPost(long id)
        {
            var result = Repository.PostRepository.DeletePostFromIdPost(id);
            return result == false ? Conflict() : Ok();
        }
    }
}