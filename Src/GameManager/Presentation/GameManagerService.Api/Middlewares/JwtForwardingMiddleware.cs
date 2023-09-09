using System.Net.Http.Headers;
using System.Net;
using GameManagerService.Application.Interfaces;
using GameManagerService.Common.Constants;
using GameManagerService.Common.Settings;

namespace GameManagerApi.Middlewares {
    public class JwtForwardingMiddleware {
        readonly RequestDelegate _next;
        public JwtForwardingMiddleware(RequestDelegate next) {
            _next = next;
        }
        public async Task Invoke(HttpContext context, ITokenService tokenService) {
            var routeDetails = context.GetEndpoint();
            if (routeDetails == null) {
                goto Execute;
            }
            string authorizationHeader = context.Request.Headers[HeaderConstants.Authorization].FirstOrDefault();
            if (authorizationHeader == null) {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            string token = authorizationHeader.Replace(HeaderConstants.Bearer, string.Empty);
            if (!string.IsNullOrEmpty(token)) {
                using (HttpClient httpClient = new HttpClient()) {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var validationResponse = await httpClient.GetAsync(
                        $"{AuthenticationApiSettings.ApiBaseUrl}{AuthenticationApiEndpointConstants.ValidateToken}");
                    if (!validationResponse.IsSuccessStatusCode) {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }
                    else {
                        var claimsPrinciple = tokenService.GetClaimsPrincipalFromToken(token);
                        context.User = claimsPrinciple;
                    }
                }
            }
        Execute:
            await _next.Invoke(context);
        }
    }
}
