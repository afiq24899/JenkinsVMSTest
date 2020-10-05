using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lingkail.VMS.Models;
using Lingkail.VMS.Data;

namespace Lingkail.VMS.Pages.EditorV2.IFrameElement
{
    public class TraveltimeModel : PageModel
    {
        private readonly SenaVMSContext _context;

        public TraveltimeModel(
            SenaVMSContext context)
        {
            _context = context;
        }
        
        [BindProperty] public Board Board { get; set; }
        public List<TravelTimeInfos_B1s> TravelTimeInfos { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == 0) { return new JsonResult("Not applicable for multiple editing"); }
            else
            {
                Board = await _context.Boards
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id); //Get board id from parent page (index) to this child page 
                                                       //Using string query from iframe - <iframe src="/EditorV2/Traveltime?id=x"></iframe>

                TravelTimeInfos = await _context.TravelTimeInfos_B1s
                    .AsNoTracking()
                    .ToListAsync(); //Get the list of travel time info (eta and dest to be rendered on the page)

                return Page();
            }
        }
    }
}