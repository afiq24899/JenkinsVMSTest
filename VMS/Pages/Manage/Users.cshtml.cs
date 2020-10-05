using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<VmsUser> userManager;

        public UsersModel(UserManager<VmsUser> userManager)
        {
            this.userManager = userManager;
        }
        public IEnumerable<VmsUser> Users { get; set; }

        public void OnGetAsync()
        {
            this.Users = userManager.Users.ToList();

        }

    }
}
