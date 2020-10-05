using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Lingkail.VMS.Pages.ConfigurationPage
{
    public class DeleteModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public DeleteModel(SenaVMSContext context) 
        {
            _context = context;
        }

        [BindProperty] public Board Board { get; set; }
        [BindProperty] public Display Display { get; set; }
        [BindProperty] public Location Location { get; set; }

        public int? Boardid { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            Board = await _context.Boards
                .AsNoTracking()
                .Include(d => d.Display)
                .Include(l => l.Location)
                .FirstOrDefaultAsync(b => b.ID == id);

            //Board = await _context.Boards.FindAsync(id);

            if (Board == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Board = await _context.Boards
                .AsNoTracking()
                .Include(d => d.Display)
                .Include(l => l.Location)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (Board != null)
            {
                _context.Boards.Remove(Board);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
