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

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class UserRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }

    }
    public class EditUserInRoleModel : PageModel
    {
        private readonly UserManager<VmsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
         public EditUserInRoleModel(UserManager<VmsUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public List<UserRole> UserRoleList_ { get; set; } = new List<UserRole>();
        [BindProperty]
        public List<string> ListOfUsers_InRole { get; set; } = new List<string>();
        [BindProperty]
        public List<UserRole> UserList_new { get; set; }
        [BindProperty]
        public string roleId_get { get; set; }
        [BindProperty]
        public List<string> PubListUserInRole { get; set; } 

        public async Task OnGetAsync(string roleId)
        {
            roleId_get = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            var user_role = await _userManager.GetUsersInRoleAsync(role.Name);
            //List<UserRole> UserRoleList_ = new List<UserRole>();
            //List<string> ListOfUsers_InRole = new List<string>();
            foreach (var _user_ in user_role)
            {
                ListOfUsers_InRole.Add(_user_.UserName);
            }

            foreach (var user in _userManager.Users)
            {
                UserRole usercollection = new UserRole
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (ListOfUsers_InRole.Contains(user.UserName))
                {
                    usercollection.IsSelected = true;
                }
                else
                {
                    usercollection.IsSelected = false;

                }
                UserRoleList_.Add(usercollection);
            }
            UserList_new = UserRoleList_;
        }
        public async Task<IActionResult> OnPostAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var user_role = await _userManager.GetUsersInRoleAsync(role.Name);
            List<string> ListUser = new List<string>();
            foreach (var _user_ in user_role)
            {
                ListUser.Add(_user_.UserName);
            }
            for (int i = 0; i < UserList_new.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(UserList_new[i].UserId);
                //Debug.WriteLine(user.UserName);
                //Debug.WriteLine(user.Email);
                //Debug.WriteLine(user.Id);
                IdentityResult result = null;
                if (UserList_new[i].IsSelected && !ListUser.Contains(user.UserName))
                {
                    Debug.WriteLine("Add " + user.UserName);
                    Debug.WriteLine("test " + role.Name);
                    try
                    {
                        result = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    
                }
                else if (!(UserList_new[i].IsSelected) && ListUser.Contains(user.UserName))
                {

                    Debug.WriteLine("Remove " + user.UserName);
                    result =await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                //if(result.Succeeded)
                //{
                //    if (i < (UserList_new.Count - 1))
                //        continue;
                //    else
                //        return new RedirectToPageResult("EditRoles", new { ID = roleId });

                //} 
            }
            return new RedirectToPageResult("EditRoles", new {ID = roleId });

        }
    }
}