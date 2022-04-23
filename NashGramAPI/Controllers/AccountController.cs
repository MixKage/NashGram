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

        [HttpPut("/CreateAccount")]
        public IActionResult CreateAccount([FromBody] AccountCreateInput input)
        {
            var result = Repository.AccountRepository.CreateAccount(input);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpPut("/UpdateLogin")]
        public IActionResult UpdateLogin([FromBody] AccountUpdateInput input)
        {
            var result = Repository.AccountRepository.UpdateLogin(input);
            return result == false ? Conflict() : Ok();
        }

        [HttpPut("/UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] AccountUpdateInput input)
        {
            var result = Repository.AccountRepository.UpdatePassword(input);
            return result == false ? Conflict() : Ok();
        }

        [HttpGet("/GetAccountFromId")]
        public IActionResult GetAccountFromId(long id)
        {
            var result = Repository.AccountRepository.GetAccountFromID(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetLogin")]
        public IActionResult GetLogin(long id)
        {
            var result = Repository.AccountRepository.GetLogin(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetPassword")]
        public IActionResult GetPassword(long id)
        {
            var result = Repository.AccountRepository.GetPassword(id);
            return result == null ? Conflict(result) : Ok(result);
        }        

        [HttpDelete("/DeleteAccountFromID")]
        public IActionResult DeleteAccountFromID(long id)
        {
            var result = Repository.AccountRepository.DeleteAccountFromID(id);
            return result == false ? Conflict() : Ok();
        }
    }
}