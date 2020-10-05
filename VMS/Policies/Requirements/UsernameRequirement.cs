using Microsoft.AspNetCore.Authorization;

namespace Lingkail.VMS.Auth.Web.Policies.Requirements
{
    public class UsernameRequirement : IAuthorizationRequirement
    {
        public UsernameRequirement(string usernamePattern)
        {
            UsernamePattern = usernamePattern;
        }

        public string UsernamePattern { get; }
    }
}
