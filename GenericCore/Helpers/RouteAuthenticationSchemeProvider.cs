using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GenericCore.Helpers
{
    public class RouteAuthenticationSchemeProvider : AuthenticationSchemeProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RouteAuthenticationSchemeProvider(
            IHttpContextAccessor httpContextAccessor,
            IOptions<AuthenticationOptions> options)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private Task<AuthenticationScheme> GetRequestSchemeAsync()
        {
            bool isAPIRequest = _httpContextAccessor.HttpContext.Request.Path.Value.ToString().Contains("api");

            var jwtAuthSchemeString = JwtBearerDefaults.AuthenticationScheme;
            var jwtAuthScheme = GetSchemeAsync(jwtAuthSchemeString);

            var cookieAuthSchemeString = CookieAuthenticationDefaults.AuthenticationScheme;
            var cookieAuthScheme = GetSchemeAsync(cookieAuthSchemeString);

            return isAPIRequest ? jwtAuthScheme : cookieAuthScheme;
        }

        public override async Task<AuthenticationScheme> GetDefaultAuthenticateSchemeAsync() => await GetRequestSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultChallengeSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultChallengeSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultForbidSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultForbidSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultSignInSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultSignInSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultSignOutSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultSignOutSchemeAsync();

    }
}
