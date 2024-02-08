using System;
using System.Security.Claims;

namespace GooglePay.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string? GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            // Handle the case where parsing fails or the value is null.
            // You might want to throw an exception or return a default value.
            throw new InvalidOperationException("Unable to retrieve user ID from claims.");
        }
    }
}
