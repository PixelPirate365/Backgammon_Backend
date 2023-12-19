using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestAuthServerController : ControllerBase {
        
        [HttpGet(nameof(GetTestResult))]
        public ActionResult GetTestResult() {

            List<string> claimTypes = new();
            List<string> claimValues = new();
            var claims = User.Claims.ToList();
            foreach (var claim in claims) {
                claimTypes.Add(claim.Type);
                claimValues.Add(claim.Value);
            }
            return Ok("Api is secured");
        }
    }
}
