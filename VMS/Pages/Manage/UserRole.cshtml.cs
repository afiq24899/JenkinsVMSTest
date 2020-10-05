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
using Microsoft.AspNetCore.Mvc;
using IdentityModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class UserRoleView
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }

    }
    public class UserRoleModel : PageModel
    {
        private readonly UserManager<VmsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleModel(UserManager<VmsUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public List<UserRoleView> RoleList { get; set; } = new List<UserRoleView>();
        [BindProperty]
        public List<string> ListOfRole { get; set; } = new List<string>();
        [BindProperty]
        public string User_ID { get; set; }
        public async Task OnGetAsync(string userid)
        {
            User_ID = userid;
            var user = await _userManager.FindByIdAsync(userid);
            var role_user = await _userManager.GetRolesAsync(user);
            ListOfRole = (List<string>)role_user;
            foreach (var role in _roleManager.Roles)
            {
                UserRoleView rolecollection = new UserRoleView
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (ListOfRole.Contains(role.Name))
                {
                    rolecollection.IsSelected = true;
                }
                else
                {
                    rolecollection.IsSelected = false;

                }
                RoleList.Add(rolecollection);
            }
        }
        public async Task<IActionResult> OnPostAsync(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            var role_user = await _userManager.GetRolesAsync(user);
            try
            {
                var result = await _userManager.RemoveFromRolesAsync(user, role_user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user existing role");
                    return new RedirectToPageResult("Error");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
            try
            {
                var result = await _userManager.AddToRolesAsync(user, RoleList.Where(x => x.IsSelected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected roles to user");
                    return new RedirectToPageResult("Error");
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return new RedirectToPageResult("EditUsers", new { ID = userid });
        }
    }
}