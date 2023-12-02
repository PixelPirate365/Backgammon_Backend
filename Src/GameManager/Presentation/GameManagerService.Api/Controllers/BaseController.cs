using Microsoft.AspNetCore.Mvc;

namespace GameManagerApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T> {
        private ILogger<T>? _logger;
        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>()!;
    }
}
