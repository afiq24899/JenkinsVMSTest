using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class EditUsersModel : PageModel
    {
        
        private readonly UserManager<VmsUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public EditUsersModel(UserManager<VmsUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        //public Task<VmsUser> User_ID { get; private set; }

        //public EditUsersModel()
        //{
        //    Claims = new List<string>();
        //    Roles = new List<string>();
        //}

        //public IEnumerable<VmsUser> Users { get; set; }
        //public IEnumerable<VmsUser> Users { get; set; }


        //[Required]
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public List<string> Roles { get; set; }

        //public List<string> Claims { get; set; }
        //public List<string> Roles { get; set; }
        //public async Task OnGetAsync()
        //{
        //    var user = await _userManager.FindByIdAsync("975c96e2-85a4-46ac-8211-53c8ebae0e87");
        //    if (user != null)
        //    {
        //        var name = user.UserName;
        //    }
        //}
        public async Task OnGetAsync(string id)
        {
            var users = await userManager.FindByIdAsync(id);
            var user_roles = await userManager.GetRolesAsync(users);
            Id = id;
            UserName = users.UserName;
            Email = users.Email;
            Roles = (List<string>)user_roles;
        }



        public async Task<RedirectToPageResult> OnPostAsync()
        {
            //Id = id;
            //Debug.WriteLine("Id in Post function: " + Id);
            //Debug.WriteLine("UserName in Post function: " + UserName);
            var users = await userManager.FindByIdAsync(Id);
            users.UserName = UserName;
            users.Email = Email;
            var result = await userManager.UpdateAsync(users);
            if (result.Succeeded)
            {
                return new RedirectToPageResult("Users");
            }
            else
            {
                return new RedirectToPageResult("Error");
            }

        }




    }
}