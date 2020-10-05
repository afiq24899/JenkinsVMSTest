using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public void OnGetAsync()
        {
            this.Roles = roleManager.Roles.ToList();

        }
    }
}