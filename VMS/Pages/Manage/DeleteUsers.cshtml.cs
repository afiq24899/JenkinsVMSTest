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
    public class DeleteUsersModel : PageModel
    {
        private readonly UserManager<VmsUser> userManager;

        public DeleteUsersModel(UserManager<VmsUser> userManager)
        {
            this.userManager = userManager;
        }


        public string Id { get; set; }
        public string UserName { get; set; }

        public async Task OnGet(string id)
        {
            var users = await userManager.FindByIdAsync(id);
            UserName = users.UserName;
            Id = id;

        }
        public async Task<IActionResult> OnPostAsync(string ID_del)
        {
            var users = await userManager.FindByIdAsync(ID_del);
            await userManager.DeleteAsync(users);
            return new RedirectToPageResult("Users");
        }  
    }
}