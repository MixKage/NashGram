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

        [HttpGet("/GetEmailFromId")]
        public IActionResult GetEmailFromId(long input)
        {
            var result = Repository.PersonRepository.GetEmailFromId(input);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetNameFromId")]
        public IActionResult GetNameFromId(long input)
        {
            var result = Repository.PersonRepository.GetNameFromId(input);
            return result == null ? Conflict() : Ok();
        }

        [HttpGet("/GetStatusFromId")]
        public IActionResult GetStatusFromId(long input)
        {
            var result = Repository.PersonRepository.GetStatusFromId(input);
            return result == null ? Conflict() : Ok();
        }

        [HttpGet("/GetCountryFromId")]
        public IActionResult GetCountryFromId(long id)
        {
            var result = Repository.PersonRepository.GetCountryFromId(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetAgeFromId")]
        public IActionResult GetAgeFromId(long id)
        {
            var result = Repository.PersonRepository.GetAgeFromId(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetNumberFromId")]
        public IActionResult GetNumberFromId(long id)
        {
            var result = Repository.PersonRepository.GetNumberFromId(id);
            return result == null ? Conflict(result) : Ok(result);
        }

        [HttpGet("/GetPersonFromID")]
        public IActionResult GetPersonFromID(long id)
        {
            var result = Repository.PersonRepository.GetPersonFromID(id);
            return result == null ? Conflict() : Ok(result);
        }
    }
}