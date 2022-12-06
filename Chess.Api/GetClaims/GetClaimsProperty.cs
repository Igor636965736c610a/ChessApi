using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chess.Api.GetClaims
{
    public static class GetClaimsProperty
    {
        public static Guid GetUserId(HubCallerContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity; ;
            return new Guid(identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
        public static string GetName(HubCallerContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity; ;
            return identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        }
        public static string GetUserEmail(HubCallerContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}