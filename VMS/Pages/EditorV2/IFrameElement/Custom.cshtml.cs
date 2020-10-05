using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using static Lingkail.VMS.Models.Services;

namespace Lingkail.VMS.Pages.EditorV2.IFrameElement
{
    public class CustomModel : PageModel
    {

        private readonly Lingkail.VMS.Data.SenaVMSContext _context;

        public CustomModel(
            Lingkail.VMS.Data.SenaVMSContext context)
        {
            _context = context;
        }

        [BindProperty] public Board Board { get; set; }
        [BindProperty] public List<Incident> Incidents { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == 0) { return new JsonResult ( "Not applicable for multiple editing"); }
            else { 
            Board = await _context.Boards
                .Include(b => b.Display)
              .AsNoTracking()
              .FirstOrDefaultAsync(m => m.ID == id); //Get board id from parent page (index) to this child page 
                                                     //Using string query from iframe - <iframe src="/EditorV2/Custom?id=x"></iframe>

            Incidents = _context.Incidents
                .AsNoTracking()
                .ToList();

                return Page();
            }

        }

        //Method post for clearing alert for board that has "alert == true" in db
        public async Task<IActionResult> OnPostClearAlert()
        {
            int alertonboard = Board.ID; 

            var alertBoarddb = await _context.Boards
                .Include(b => b.Display)
                .FirstOrDefaultAsync(m => m.ID == alertonboard);

            alertBoarddb.Display.HasAlert = false;

            _context.Update(alertBoarddb);
            _context.SaveChanges();

            return Page();
        }
    }
}