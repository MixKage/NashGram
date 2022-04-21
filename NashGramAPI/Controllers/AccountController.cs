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

        //[HttpGet(Name = "GetCreateAccount1")]
        //public IActionResult CreateAccount1([FromBody] AccountCreateInput input)
        //{
        //    var result = Repository.AccountRepository.CreateAccount(input);
        //    return result == null ? Conflict(result) : Ok(result);
        //}

        //[HttpGet(Name = "GetUpdateLogin1")]
        //public IActionResult UpdateLogin1([FromBody] AccountUpdateInput input)
        //{
        //    var result = Repository.AccountRepository.UpdateLogin(input);
        //    return result == false ? Conflict() : Ok();
        //}

        //[HttpGet(Name = "GetUpdatePassword1")]
        //public IActionResult UpdatePassword1([FromBody] AccountUpdateInput input)
        //{
        //    var result = Repository.AccountRepository.UpdatePassword(input);
        //    return result == false ? Conflict() : Ok();
        //}

        //[HttpGet(Name = "GetAccountFromId1")]
        //public IActionResult GetAccountFromId1(long id)
        //{
        //    var result = Repository.AccountRepository.GetAccountFromID(id);
        //    return result == null ? Conflict(result) : Ok(result);
        //}

        [HttpGet(Name = "/Login")]
        public IActionResult GetLogin(long id)
        {
            var result = Repository.AccountRepository.GetLogin(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/Password")]
        public IActionResult GetPassword(long id)
        {
            var result = Repository.AccountRepository.GetPassword(id);
            return result == null ? Conflict(result) : Ok(result);
        }        

        [HttpDelete(Name = "DeleteAccountFromID")]
        public IActionResult DeleteAccountFromID(long id)
        {
            var result = Repository.AccountRepository.DeleteAccountFromID(id);
            return result == false ? Conflict(result) : Ok(result);
        }
    }
}