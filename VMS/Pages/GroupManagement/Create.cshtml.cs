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
    public class CreateModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public CreateModel(SenaVMSContext context) 
        {
            _context = context;
        }

        [BindProperty] public GroupPreset GroupPreset { get; set; }
        public IList<Board> BoardList { get; set; }
        public IList<Camera> CameraList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BoardList = await _context.Boards
                .Include(b => b.Location)
                .AsNoTracking()
                .ToListAsync();
            CameraList = await _context.Cameras.ToListAsync();

            return Page(); 
        }

        public async Task<IActionResult> OnPost(string[] selectedBoards, string[] selectedCameras)
        {
            var thisPreset = GroupPreset;

            _context.GroupPresets.Add(
                new GroupPreset
                { 
                    Name = thisPreset.Name,
                    DateTimeCreated = DateTime.Now
                });
            await _context.SaveChangesAsync();


            IQueryable<GroupPreset> presetsIQ = from gp in _context.GroupPresets select gp;
            presetsIQ = presetsIQ.OrderByDescending(b => b.ID); //Sort ID in Decending order
            var presetListDec = await presetsIQ.AsNoTracking().ToListAsync();
            var latestPreset = presetListDec.FirstOrDefault();

            var boardGroupList = new List<BoardGroupingAssignment>();
            var cameraGroupList = new List<CameraGroupingAssignment>();

            foreach (string selectedBoardId in selectedBoards)
            {
                BoardGroupingAssignment bga = new BoardGroupingAssignment();
                bga.GroupPresetID = latestPreset.ID;
                bga.BoardID = int.Parse(selectedBoardId);
                boardGroupList.Add(bga);
            }
            _context.BoardGroupingAssignments.AddRange(boardGroupList);

            foreach (string selectedCameraId in selectedCameras)
            {
                CameraGroupingAssignment cga = new CameraGroupingAssignment();
                cga.GroupPresetID = latestPreset.ID;
                cga.CameraID = int.Parse(selectedCameraId);
                cameraGroupList.Add(cga);
            }
            _context.CameraGroupingAssignments.AddRange(cameraGroupList);


            await _context.SaveChangesAsync();
            //return Page(); 
            return RedirectToPage("./Index"); 
        }
    }
}