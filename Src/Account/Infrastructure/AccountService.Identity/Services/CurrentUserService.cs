using AccountService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AccountService.Identity.Services {
    public class CurrentUserService : ICurrentUserService {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        private string UserIdentifier => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public Guid? UserId => string.IsNullOrWhiteSpace(UserIdentifier) ? null : Guid.Parse(UserIdentifier);

    }
}
