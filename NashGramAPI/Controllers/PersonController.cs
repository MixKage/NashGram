using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPut("/UpdateEmailFromId")]
        public IActionResult UpdateEmailFromId([FromBody] AccountUpdateInput input)
        {
            var result = Repository.PersonRepository.UpdateInfoFromId(input, 0);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateNameFromId")]
        public IActionResult UpdateNameFromId([FromBody] AccountUpdateInput input)
        {
            var result = Repository.PersonRepository.UpdateInfoFromId(input, 1);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateStatusFromId")]
        public IActionResult UpdateStatusFromId([FromBody] AccountUpdateInput input)
        {
            var result = Repository.PersonRepository.UpdateInfoFromId(input, 2);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateCountryFromId")]
        public IActionResult UpdateCountryFromId([FromBody] AccountUpdateInput input)
        {
            var result = Repository.PersonRepository.UpdateInfoFromId(input, 3);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateAgeFromId")]
        public IActionResult UpdateAgeFromId([FromBody] AccountUpdateInput input)
        {
            var result = Repository.PersonRepository.UpdateInfoFromId(input, 4);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateNumberFromId")]
        public IActionResult UpdateNumberFromId([FromBody] AccountUpdateInput input)
        {
            var result = Repository.PersonRepository.UpdateInfoFromId(input, 5);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpGet("/GetEmailFromId")]
        public IActionResult GetEmailFromId(long input)
        {
            var result = Repository.PersonRepository.GetEmailFromId(input);
            return result == null ? Conflict(result) : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetNameFromId")]
        public IActionResult GetNameFromId(long input)
        {
            var result = Repository.PersonRepository.GetNameFromId(input);
            return result == null ? Conflict() : Ok();
        }

        [Authorize]
        [HttpGet("/GetStatusFromId")]
        public IActionResult GetStatusFromId(long input)
        {
            var result = Repository.PersonRepository.GetStatusFromId(input);
            return result == null ? Conflict() : Ok();
        }

        [Authorize]
        [HttpGet("/GetCountryFromId")]
        public IActionResult GetCountryFromId(long id)
        {
            var result = Repository.PersonRepository.GetCountryFromId(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetAgeFromId")]
        public IActionResult GetAgeFromId(long id)
        {
            var result = Repository.PersonRepository.GetAgeFromId(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetNumberFromId")]
        public IActionResult GetNumberFromId(long id)
        {
            var result = Repository.PersonRepository.GetNumberFromId(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetPersonFromID")]
        public IActionResult GetPersonFromID(long id)
        {
            var result = Repository.PersonRepository.GetPersonFromID(id);
            return result == null ? Conflict() : Ok(result);
        }
    }
}