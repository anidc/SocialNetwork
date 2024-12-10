using System.Security.Claims;

namespace SocialNetwork.Helpers
{
    public static class ClaimsHelper
    {
        public static string GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                throw new ArgumentNullException(nameof(claimsPrincipal), "ClaimsPrincipal cannot be null.");

            var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out _))
            {
                return userId;
            }

            throw new InvalidOperationException("The user ID claim is missing or invalid.");
        }
    }
}