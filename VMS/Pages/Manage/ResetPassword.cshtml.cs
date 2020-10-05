using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Lingkail.VMS.Pages.Manage
{
    [Authorize(Roles = "admin")]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<VmsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ResetPasswordModel(UserManager<VmsUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string user_name { get; set; }

        public void OnGet(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            user_name = user.UserName;
        }

        public async Task<RedirectToPageResult> OnPostAsync(string id)
        {
            if(Password == ConfirmPassword)
            {
                Debug.WriteLine("New pass: " + Password);
                Debug.WriteLine("New confirmed pass: " + ConfirmPassword);
            }    
            

            var user = await _userManager.FindByIdAsync(id);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var result = await _userManager.ResetPasswordAsync(user, token, Password);

            //Debug.WriteLine(": " + Password);
            return new RedirectToPageResult("Users");
        }
    }
}