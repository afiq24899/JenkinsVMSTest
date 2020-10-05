using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lingkail.VMS.Pages.ConfigurationPage
{
    public class EditModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public EditModel(SenaVMSContext context) 
        {
            _context = context;
        }

        [BindProperty] public Board Board { get; set; }
        [BindProperty] public Display Display { get; set; }
        [BindProperty] public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            Board = await _context.Boards
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ID == id);

            if (Board == null)
            {
                return NotFound();
            }

            Display = await _context.Displays
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BoardID == id);

            if (Display == null)
            {
                return NotFound();
            }

            Location = await _context.Locations
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BoardID == id);

            if (Board == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            //Update Boards in database
            var newboard = Board;

            _context.Boards.Update(
                new Board
                {
                    ID = Board.ID,
                    Name = newboard.Name,
                    ZoneID = newboard.ZoneID,
                    IsAtSite = false

                });

            await _context.SaveChangesAsync();

            //Update Displays in database
            var newdisplay = Display;

            _context.Displays.Update(
               new Display
               {
                   BoardID = Board.ID,
                   C4_IP = newdisplay.C4_IP,
                   CCTV = newdisplay.CCTV,
                   PTZ = newdisplay.PTZ,
                   InstallationDate = DateTime.Now,
                   OperationalStatus = newdisplay.OperationalStatus
               });

            await _context.SaveChangesAsync();

            //Update Locations in database
            var newlocation = Location;

            _context.Locations.Update(
                new Location
                {
                    BoardID = Board.ID,
                    Address = newlocation.Address,
                    Longitude = newlocation.Longitude,
                    Latitude = newlocation.Latitude

                });

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}