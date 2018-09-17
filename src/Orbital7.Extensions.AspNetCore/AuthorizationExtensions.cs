using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.AspNetCore.Authorization
{
    public static class AuthorizationExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity; 

            var first = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            if (first != null)
                return Guid.Parse(first.Value);
            else
                return Guid.Empty;
        }
    }
}
