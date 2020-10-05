using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
namespace Lingkail.VMS.Pages.Manage
{
    public class DeleteRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public DeleteRolesModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public string Id { get; set; }
        public string RoleName { get; set; }
        public async Task OnGet(string id)
        {
            var roles = await roleManager.FindByIdAsync(id);
            RoleName = roles.Name;
            Id = id;

        }
        public async Task<IActionResult> OnPostAsync(string ID_del)
        {
            var roles = await roleManager.FindByIdAsync(ID_del);
            await roleManager.DeleteAsync(roles);
            return new RedirectToPageResult("Roles");
        }
    }
}