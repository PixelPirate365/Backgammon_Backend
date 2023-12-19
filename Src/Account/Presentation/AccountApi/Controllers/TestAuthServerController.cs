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

            return Ok("Api is secured");
        }
    }
}
