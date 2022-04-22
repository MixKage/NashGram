using Microsoft.AspNetCore.Mvc;
using NashGramAPI.Model;
using NashGramAPI.Repository;

using static NashGramAPI.Model.ModelClass;

namespace NashGramAPI.Controllers
{
    [Route("Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        [HttpGet("/GetAuthorFromId")]
        public IActionResult GetAuthor(long input)
        {
            var result = Repository.PostRepository.GetAuthor(input);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetUriFromId")]
        public IActionResult GetUri(long input)
        {
            var result = Repository.PostRepository.GetUri(input);
            return result == null ? Conflict() : Ok();
        }

        [HttpGet("/GetDescryptionFromId")]
        public IActionResult GetDescryption(long input)
        {
            var result = Repository.PostRepository.GetDescryption(input);
            return result == null ? Conflict() : Ok();
        }

        [HttpGet("/GetTagFromId")]
        public IActionResult GetTag(long id)
        {
            var result = Repository.PostRepository.GetTag(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetPostFromIdPost")]
        public IActionResult GetPostFromIdPost(long id)
        {
            var result = Repository.PostRepository.GetPostFromIdPost(id);
            return result == null ? Conflict(result) : Ok(result);
        }
    }
}