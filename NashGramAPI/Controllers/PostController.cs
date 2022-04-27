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
        [HttpPost("/UpdateDescryptionFromIdPost")]
        public IActionResult UpdateDescryptionFromIdPost([FromBody] ModelClass.UpdateInput updateInput)
        {
            var result = Repository.PostRepository.UpdateInfoFromIdPost(updateInput, 1);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPost("/UpdateTagFromIdPost")]
        public IActionResult UpdateTagFromIdPost([FromBody] ModelClass.UpdateInput updateInput)
        {
            var result = Repository.PostRepository.UpdateInfoFromIdPost(updateInput, 2);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPost("/CreatePost")]
        public IActionResult CreatePost([FromBody] ModelClass.PostCreate input)
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
        [HttpGet("/GetImageFromId")]
        public IActionResult GetImage(long input)
        {
            var result = Repository.PostRepository.GetImage(input);
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

        [Authorize]
        [HttpGet("/GetAllMyPosts")]
        public IActionResult GetAllMyPosts()
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();            

            var result = Repository.PostRepository.GetPostsFromIdAccount((long)id);
            return result == null ? Conflict() : Ok(result);
        }

        [HttpGet("/GetAllPosts")]
        public IActionResult GetAllPosts()
        {
            var result = Repository.PostRepository.GetAllPosts();
            return result == null ? Conflict() : Ok(result);
        }

        [HttpGet("/GetAllPostsByTag")]
        public IActionResult GetAllPosts(string tag)
        {
            var result = Repository.PostRepository.GetAllPostsByTag(tag);
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