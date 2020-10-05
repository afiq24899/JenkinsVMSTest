using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; //Use EF Core
using Lingkail.VMS.Models;

namespace Lingkail.VMS.Pages.VMS
{
    public class TableModel : PageModel
    {
        private readonly Data.SenaVMSContext _context;
        public TableModel(Data.SenaVMSContext context)
        {
            _context = context;
        }

        //---------------This section is to display alert message only--------------------//
        //Upon successful editing and saving, message will pop up to notify- 'please check history'
        [TempData] public string Message { get; set; }
        [TempData] public string ErrorMessage { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        //---------------This section is to display alert message only--------------------//

        public IList<Board> Boards { get; set; }
        public string fileExtension = ".jpg";

        //---------------------GroupPresetFilter--------------------------------------//
        public IList<GroupPreset> GroupPresets { get; set; }
        public string SelectedPresetBoard { get; set; }
        public int[] thisBoardID { get; set; }
        public string[] thisBoardNames { get; set; }




        public async Task OnGetAsync(int? id)
        {
            IQueryable<Board> boardsIQ = from b in _context.Boards
                                .Include(b => b.Zone)
                                .Include(b => b.Location)
                                .Include(b => b.Display)
                                    .ThenInclude(d => d.MessageAssignments)
                                         select b;
            boardsIQ = boardsIQ.OrderBy(b => b.ID); //Sort ID in ascending order

            Boards = await boardsIQ.AsNoTracking().ToListAsync();



            GroupPresets = await _context.GroupPresets
                           .Include(p => p.BoardGroupingAssignments)
                           .ToListAsync();





            if (id != null)
            {


                thisBoardID = await _context.BoardGroupingAssignments  //list of board ids
                    .Where(b => b.GroupPreset.ID == id)
                    .Select(b => b.BoardID).ToArrayAsync();

                thisBoardNames = await _context.BoardGroupingAssignments  //list of board names
                .Where(b => b.GroupPreset.ID == id)
                .Select(b => b.Board.Name).ToArrayAsync();


                string[] NamesArray = new string[thisBoardNames.Length];
                for (int i = 0; i < thisBoardNames.Length; i++)
                {

                   NamesArray[i] = thisBoardNames[i];
                }


                string viewManyBoardNames = string.Join(", ", thisBoardNames); //Convert string array (Board Names) to comma-separated string


                SelectedPresetBoard = viewManyBoardNames;



                //When an IQueryable is created or modified, no query is sent to the database. 
                //The IQueryable code results in a single query that's not executed until the LAST statement.
            }

        }
    }
}


