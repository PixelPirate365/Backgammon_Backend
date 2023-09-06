using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Interfaces {
    public interface ITokenService {
        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
    }
}
