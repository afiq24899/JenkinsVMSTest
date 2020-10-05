using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class EditRolesModel : PageModel
    {
        private readonly UserManager<VmsUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;  
        public EditRolesModel(UserManager<VmsUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            Users_Role = new List<string>();
        }
        [BindProperty]
        public string RoleID { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
        [BindProperty]
        public List<string> Users_Role { get; set; }
        public object mode_post { get; private set; }

        public async Task OnGetAsync(string id)
        {
            var roles = await roleManager.FindByIdAsync(id);
            RoleID = id;
            //Debug.WriteLine("RoleID in Get function: " + RoleID);
            RoleName = roles.Name;
            //Debug.WriteLine("RoleName in Get function: " + RoleName);
            var user_role = await userManager.GetUsersInRoleAsync(roles.Name);
            foreach (var user in user_role)
            {
                //Debug.WriteLine(user.UserName);
                Users_Role.Add(user.UserName);
            }
        }
        public async Task<RedirectToPageResult> OnPostAsync(string id)
        {
            RoleID = id;
            Debug.WriteLine("RoleID in Post function: " + RoleID);
            Debug.WriteLine("RoleName in Post function: " + RoleName);
            var role = await roleManager.FindByIdAsync(RoleID);
            role.Name = RoleName;
            Debug.WriteLine(RoleName);
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new RedirectToPageResult("Roles");
            }
            else
            {
                return new RedirectToPageResult("Error");
            }
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError("", error.Description);
            //}
        }
    }
}