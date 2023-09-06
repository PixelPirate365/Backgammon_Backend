using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.Constants {
    public static class AuthenticationApiEndpointConstants {
        private const string Api = "/api";
        public const string ValidateToken = $"{Api}/User/ValidateToken";
    }
}
