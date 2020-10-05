using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lingkail.VMS.Pages.GroupManagement
{
    public class DetailsModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public DetailsModel(SenaVMSContext context) 
        {
            _context = context;
        }

        public GroupPreset GroupPreset { get; set; }
        public IList<Board> GroupMembers { get; set; }
        public IList<Camera> Cameras { get; set; }

        public async Task<IActionResult> OnGetAsync(int? presetid)
        {
            if (presetid == null) return NotFound(); 

            GroupPreset = await _context.GroupPresets
               .Include(gp => gp.BoardGroupingAssignments)
                    .ThenInclude(bg=>bg.Board)
                        .ThenInclude(b=>b.Zone)
               .Include(gp => gp.BoardGroupingAssignments)
                    .ThenInclude(bg => bg.Board)
                        .ThenInclude(b => b.Location)
               .Include(gp => gp.CameraGroupingAssignments)
                    .ThenInclude(cg=>cg.Camera)
               .AsNoTracking()
               .FirstOrDefaultAsync(gp => gp.ID == presetid);

            GroupMembers = GroupPreset.BoardGroupingAssignments
                .Select(bg => bg.Board)
                .ToList();
            Cameras = GroupPreset.CameraGroupingAssignments
                .Select(cg => cg.Camera)
                .ToList();

            return Page();
        }

    }
}