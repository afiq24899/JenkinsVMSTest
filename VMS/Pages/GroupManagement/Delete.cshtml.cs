using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Lingkail.VMS.Pages.GroupManagement
{
    public class DeleteModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public DeleteModel(SenaVMSContext context) 
        {
            _context = context;
        }

        [BindProperty] public GroupPreset GroupPreset { get; set; }

        public async Task<IActionResult> OnGet(int? presetid) 
        {
            if (presetid == null) return NotFound();

            GroupPreset = await _context.GroupPresets.FindAsync(presetid);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            GroupPreset groupPresetToDelete = await _context.GroupPresets
                .FindAsync(GroupPreset.ID); 

            try
            {
                _context.GroupPresets.Remove(groupPresetToDelete);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                return new JsonResult("Cannot update!"); 
            }
            return RedirectToPage("./Index");
        }
    }
}