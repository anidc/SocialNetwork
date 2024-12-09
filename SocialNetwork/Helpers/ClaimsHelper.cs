using System.Security.Claims;

namespace SocialNetwork.Helpers
{
    public static class ClaimsHelper
    {
        public static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out var guid))
            {
                return guid.ToString();
            }

            throw new InvalidOperationException("No user id");
        }
    }
}