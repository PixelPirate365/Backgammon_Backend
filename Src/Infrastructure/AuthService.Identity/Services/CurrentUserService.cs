using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AuthService.Identity.Services {
    public class CurrentUserService : ICurrentUserService {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        private string UserIdentifier => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string UserId => string.IsNullOrWhiteSpace(UserIdentifier) ? null : UserIdentifier;
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserIdentifier);
    }
}
