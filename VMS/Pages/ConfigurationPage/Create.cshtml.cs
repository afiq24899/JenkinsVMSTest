using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lingkail.VMS.Pages.ConfigurationPage
{
    public class CreateModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public CreateModel(SenaVMSContext context)
        {
            _context = context;
        }

        [BindProperty] public Board Board { get; set; }
        [BindProperty] public Display Display { get; set; }
        [BindProperty] public Location Location { get; set; }

        public int Newboardid { get; set; }
        public int GetLastboardid { get; set; } 

        public void OnGet()
        {
            IQueryable<Board> boardsIQ = from b in _context.Boards
                                .Include(b => b.Location)
                                .Include(b => b.Display)
                                         select b;
            int last_boardid = _context.Boards.Max(i => i.ID);

            GetLastboardid = last_boardid + 1;

        }

        public async Task<IActionResult> OnPost()
        {

           int lastboardid = _context.Boards.Max(i => i.ID);

            Newboardid = lastboardid + 1;

            //Update Boards in databases
            var newboard = Board;

            _context.Boards.Add(
                new Board
                {
                    ID = Newboardid,
                    Name = newboard.Name,
                    ZoneID = newboard.ZoneID,
                    IsAtSite = false

                });

            await _context.SaveChangesAsync();

            //Update Displays in database
            var newdisplay = Display;

            _context.Displays.Add(
               new Display
               {
                   BoardID = Newboardid,
                   C4_IP = newdisplay.C4_IP,
                   CCTV = newdisplay.CCTV,
                   PTZ = newdisplay.PTZ,
                   InstallationDate = DateTime.Now,
                   OperationalStatus = newdisplay.OperationalStatus
               });

            await _context.SaveChangesAsync();

            //Update Locations in database
            var newlocation = Location;

            _context.Locations.Add(
                new Location
                {
                    BoardID = Newboardid,
                    Address = newlocation.Address,
                    Longitude = newlocation.Longitude,
                    Latitude = newlocation.Latitude

                });

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


    }
}