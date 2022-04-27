using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NashGramAPI.Model;
using NashGramAPI.Repository;

using static NashGramAPI.Model.ModelClass;

namespace NashGramAPI.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost("/CreateAccount")]
        public IActionResult CreateAccount([FromBody] AccountCreateInput input)
        {
            var result = Repository.AccountRepository.CreateAccount(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpPut("/UpdateLogin")]
        public IActionResult UpdateLogin(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.AccountRepository.UpdateLogin(input);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdatePassword")]
        public IActionResult UpdatePassword(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.AccountRepository.UpdatePassword(input);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpGet("/GetAccountFromId")]
        public IActionResult GetAccountFromId(long id)
        {
            var result = Repository.AccountRepository.GetAccountFromID(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetLogin")]
        public IActionResult GetLogin(long id)
        {
            var result = Repository.AccountRepository.GetLogin(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetPassword")]
        public IActionResult GetPassword(long id)
        {
            var result = Repository.AccountRepository.GetPassword(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpDelete("/DeleteAccountFromID")]
        public IActionResult DeleteAccountFromID(long id)
        {
            var result = Repository.AccountRepository.DeleteAccountFromID(id);
            return result == false ? Conflict() : Ok();
        }
    }
}