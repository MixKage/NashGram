using Microsoft.AspNetCore.Mvc;
using NashGramAPI.Model;
using NashGramAPI.Repository;

using static NashGramAPI.Model.ModelClass;

namespace NashGramAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        
        [HttpPost(Name = "PostCreateAccount")]
        public IActionResult CreateAccount([FromBody] AccountInput input)
        {
            var result = Repository.AccountRepository.CreateAccount(input);
            return result == null ? Conflict(result) : Ok(result);            
        }

        [HttpGet(Name = "GetAccountFromId")]
        public IActionResult GetAccountFromId(long id)
        {
            var result = Repository.AccountRepository.GetAccountFromID(id);
            return result == null ? Conflict(result) : Ok(result);
        }


    }
}