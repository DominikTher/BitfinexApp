using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BitfinexApp
{
    public class SimpleSecurityOptions : AuthenticationSchemeOptions
    {
    }

    public class SimpleSecurityHandler : AuthenticationHandler<SimpleSecurityOptions>
    {
        private readonly IConfiguration configuration;

        public SimpleSecurityHandler(
            IOptionsMonitor<SimpleSecurityOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            this.configuration = configuration;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var key = configuration.GetSection("Security:Key").Value;
            var token = Request.Query[key].ToString();
            var value = configuration.GetSection("Security:Value").Value;

            return token == value
                ? Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Dominik") }, Scheme.Name)), Scheme.Name)))
                : Task.FromResult(AuthenticateResult.Fail("Wrooong!"));
        }
    }
}