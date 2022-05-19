using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NashGramAPI.Model;
using NashGramAPI.Repository;

using static NashGramAPI.Model.ModelClass;

namespace NashGramAPI.Controllers
{
    [Route("Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [Authorize]
        [HttpPost("/CreateReport")]
        public IActionResult CreateAccount([FromBody] UpdateInput input)
        {
            long? id = API.BasicAuthenticationHandler.GetIdFromLogin(Request.Headers["Authorization"]);
            if (id == null) return NotFound();            

            var result = Repository.ReportRepository.CreateReport(input, (long)id);
            return result == null ? Conflict() : Ok(result);
        }
        
        [Authorize]
        [HttpGet("/GetCountReportByIdPost")]
        public IActionResult GetCountReportByIdPost(long idPost)
        {            
            var result = Repository.ReportRepository.GetCountReportByIdPost(idPost);
            return result == null ? Conflict() : Ok(result);
        }
    }
}