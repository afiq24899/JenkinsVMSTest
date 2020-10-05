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
    public class EditModel : PageModel
    {
        private readonly SenaVMSContext _context;
        public EditModel(SenaVMSContext context) 
        {
            _context = context;
        }

        [BindProperty] public GroupPreset GroupPreset { get; set; }
        public IList<AssignedBoard> AssignedBoardList { get; set; }
        public IList<AssignedCamera> AssignedCameraList { get; set; }

        public async Task<IActionResult> OnGet(int? presetid)
        {
            if (presetid == null) return NotFound();

            GroupPreset = await _context.GroupPresets
                .Include(gp => gp.BoardGroupingAssignments)
                    .ThenInclude(bg => bg.Board)
                        .ThenInclude(b => b.Location)
                .Include(gp => gp.CameraGroupingAssignments)
                    .ThenInclude(cg => cg.Camera)
                .FirstOrDefaultAsync(gp => gp.ID == presetid);

            var groupMembers_id = GroupPreset.BoardGroupingAssignments
                .Select(bg => bg.Board.ID);
            var cameras_id = GroupPreset.CameraGroupingAssignments
                .Select(cg => cg.Camera.ID);


            List<Board> allBoards = await _context.Boards
                .Include(b => b.Location)
                .ToListAsync();
            AssignedBoardList = new List<AssignedBoard>();
            foreach (var board in allBoards) 
            {
                AssignedBoardList.Add(new AssignedBoard 
                {
                    ID = board.ID,
                    Name = board.Name,
                    Address = board.Location.Address,
                    Assigned = groupMembers_id.Contains(board.ID)
                });
            }
            List<Camera> allCameras = await _context.Cameras.ToListAsync();
            AssignedCameraList = new List<AssignedCamera>();
            foreach (var camera in allCameras)
            {
                AssignedCameraList.Add(new AssignedCamera
                {
                    ID = camera.ID,
                    Name = camera.Name,
                    Assigned = cameras_id.Contains(camera.ID)
                });
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string[] selectedBoards, string[] selectedCameras)
        {
            var thisPreset = GroupPreset;
            var selectedBoardIds = Array.ConvertAll(selectedBoards, int.Parse).ToList();
            var selectedCameraIds = Array.ConvertAll(selectedCameras, int.Parse).ToList();

            //Update GroupPreset
            var presetToUpdate = await _context.GroupPresets
                    .Include(gp => gp.BoardGroupingAssignments)
                    .Include(gp => gp.CameraGroupingAssignments)
                .FirstOrDefaultAsync(gp => gp.ID == thisPreset.ID);

            presetToUpdate.Name = thisPreset.Name;
            presetToUpdate.DateTimeCreated = DateTime.Now;
            await _context.SaveChangesAsync();

            //Update BoardGroupingAssignment
            await UpdateBoardGroupingAssignments(presetToUpdate, selectedBoardIds);

            //Update CameraGroupingAssignment
            await UpdateCameraGroupingAssignments(presetToUpdate, selectedCameraIds);


            return RedirectToPage("./Index");
        }

        private async Task UpdateBoardGroupingAssignments(GroupPreset presetToUpdate, List<int> newBoardCollection) 
        {
            var oldBoardCollection = presetToUpdate.BoardGroupingAssignments
                .Select(bga => bga.BoardID).ToList(); //board ids
            var allBoards = await _context.Boards.ToListAsync();

            foreach(var thisBoard in allBoards)
            {
                if (newBoardCollection.Contains(thisBoard.ID)) //if selected 
                {
                    if (oldBoardCollection.Contains(thisBoard.ID)) //if exists in the database
                    {
                        //do nothing for now, nothing to update
                    }
                    else //if does not exist in the database
                    {
                        presetToUpdate.BoardGroupingAssignments.Add( //add
                            new BoardGroupingAssignment
                            {
                                GroupPresetID = presetToUpdate.ID,
                                BoardID = thisBoard.ID
                            });
                    }
                }
                else //if not selected
                {
                    if (oldBoardCollection.Contains(thisBoard.ID)) //if exists in the database
                    {
                        var memberToRemove = presetToUpdate.BoardGroupingAssignments
                            .FirstOrDefault(bga => bga.BoardID == thisBoard.ID);
                        presetToUpdate.BoardGroupingAssignments.Remove(memberToRemove); //remove
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
        private async Task UpdateCameraGroupingAssignments(GroupPreset presetToUpdate, List<int> newCameraCollection) 
        {
            var oldCameraCollection = presetToUpdate.CameraGroupingAssignments
                    .Select(cga => cga.CameraID).ToList(); //camera ids
            var allCameras = await _context.Cameras.ToListAsync();

            foreach (var thisCamera in allCameras)
            {
                if (newCameraCollection.Contains(thisCamera.ID)) //if selected 
                {
                    if (oldCameraCollection.Contains(thisCamera.ID)) //if exists in the database
                    {
                        //do nothing for now, nothing to update
                    }
                    else //if does not exist in the database
                    {
                        presetToUpdate.CameraGroupingAssignments.Add( //add
                            new CameraGroupingAssignment
                            {
                                GroupPresetID = presetToUpdate.ID,
                                CameraID = thisCamera.ID
                            });
                    }
                }
                else //if not selected
                {
                    if (oldCameraCollection.Contains(thisCamera.ID)) //if exists in the database
                    {
                        var cameraToRemove = presetToUpdate.CameraGroupingAssignments
                            .FirstOrDefault(cga => cga.CameraID == thisCamera.ID);
                        presetToUpdate.CameraGroupingAssignments.Remove(cameraToRemove); //remove
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
    }

    public class AssignedBoard 
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Assigned { get; set; }
    }

    public class AssignedCamera
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}