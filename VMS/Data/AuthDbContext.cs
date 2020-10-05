using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Lingkail.VMS.Auth.Web.Data
{
    public class VmsUser : IdentityUser
    {
        public List<IdentityUserRole<string>> Roles { get; set; }
    }

    public class AuthDbContext : IdentityDbContext<VmsUser>
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }
    }
}
