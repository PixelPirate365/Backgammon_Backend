using GameManagerService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Identity.Services {
    public class CurrentUserService : ICurrentUserService {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        private string UserIdentifier => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public Guid? UserId => string.IsNullOrWhiteSpace(UserIdentifier) ? null : Guid.Parse(UserIdentifier);

    }
}
