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
        [HttpPut("/UpdateEmail")]
        public IActionResult UpdateEmail(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.PersonRepository.UpdateInfoFromId(input, 0);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateName")]
        public IActionResult UpdateName(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.PersonRepository.UpdateInfoFromId(input, 1);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateStatus")]
        public IActionResult UpdateStatus(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.PersonRepository.UpdateInfoFromId(input, 2);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateCountry")]
        public IActionResult UpdateCountry(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.PersonRepository.UpdateInfoFromId(input, 3);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateAge")]
        public IActionResult UpdateAge(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.PersonRepository.UpdateInfoFromId(input, 4);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpPut("/UpdateNumber")]
        public IActionResult UpdateNumber(string textInfo)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            ModelClass.UpdateInput input = new UpdateInput((long)id, textInfo);

            var result = Repository.PersonRepository.UpdateInfoFromId(input, 5);
            return result == false ? Conflict() : Ok();
        }

        [Authorize]
        [HttpGet("/GetEmail")]
        public IActionResult GetEmail()
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();
            var result = Repository.PersonRepository.GetEmailFromId((long)id);
            return result == null ? Conflict() : Ok(result);
        }

        [HttpGet("/GetEmailFromId")]
        public IActionResult GetEmailFromId(long input)
        {
            var result = Repository.PersonRepository.GetEmailFromId(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetNameFromId")]
        public IActionResult GetNameFromId(long input)
        {
            var result = Repository.PersonRepository.GetNameFromId(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetStatusFromId")]
        public IActionResult GetStatusFromId(long input)
        {
            var result = Repository.PersonRepository.GetStatusFromId(input);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetCountryFromId")]
        public IActionResult GetCountryFromId(long id)
        {
            var result = Repository.PersonRepository.GetCountryFromId(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetAgeFromId")]
        public IActionResult GetAgeFromId(long id)
        {
            var result = Repository.PersonRepository.GetAgeFromId(id);
            return result == null ? Conflict() : Ok(result);
        }

        [Authorize]
        [HttpGet("/GetNumberFromId")]
        public IActionResult GetNumberFromId(long id)
        {
            var result = Repository.PersonRepository.GetNumberFromId(id);
            return result == null ? Conflict() : Ok(result);
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