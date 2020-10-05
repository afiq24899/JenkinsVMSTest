using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class NewRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public NewRoleModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [BindProperty]
        public RoleInput Input { get; set; }
        public string ReturnUrl { get; set; }
        public class RoleInput
        {
            [Required]
            [Display(Name = "New Role")]
            public string RoleName { get; set; }


        }


        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //Debug.WriteLine("Can reach this line");
                IdentityRole identityRole = new IdentityRole
                {
                    Name = Input.RoleName,
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return new RedirectToPageResult("Roles");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                //Debug.WriteLine("Can reach this line");
            }
            return new RedirectToPageResult("Roles");

        }
    }

}