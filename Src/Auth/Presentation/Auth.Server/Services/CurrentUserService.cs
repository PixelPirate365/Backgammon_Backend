using Auth.Server.Interfaces;
using System.Security.Claims;

namespace Auth.Server.Services {
    public class CurrentUserService : ICurrentUserService {
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
