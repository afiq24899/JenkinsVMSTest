using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Models;
//using Lingkail.VMS.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; //Use EF Core
using Newtonsoft.Json.Linq;

namespace Lingkail.VMS.Pages.VMS
{
    public class ReportModel : PageModel
    {
        //public ChartJsViewModel myChart { get; set; }
        public IList<Board> Boards { get; set; }
        public List<ReportData> ReportDate { get; set; }
        public List<UptimeReport> UpDown { get; set; } = new List<UptimeReport>();
        public int BoardUPTotal = 0;
        public int BoardDownTotal = 0;



        public List<String> BoardDown_Current { get; set; } = new List<String>();
        public List<String> BoardUp_Current { get; set; } = new List<String>();
        public string NowDownStringCollection { get; set; }
        public string NowUpStringCollection { get; set; }


        private readonly Lingkail.VMS.Data.SenaVMSContext _context;

        public ReportModel(Lingkail.VMS.Data.SenaVMSContext context)
        {
            _context = context;
        }


        /* Find Board OP from BoardID */
        public int FindBoardOP(int IDBoard)
        {
            var board_op = 0;
            foreach (var value_op in _context.Displays.Where(x => x.BoardID == IDBoard).ToListAsync().Result)
            {
                board_op = value_op.OperationalStatus;
            }
            return board_op;


        }
        /* Find Board OP from BoardID */

        /* Find Board ID from Board Name */
        public int FindBoardID(string nameboard)
        {
            var board_id = 0;
            foreach (var value_name in _context.Boards.Where(x => x.Name == nameboard).ToListAsync().Result)
            {
                board_id = value_name.ID;
            }
            return board_id;
        }
        /* Find Board ID from Board Name */

        /* Query the board with OP 1 (up) & 2(down) */
        public async Task FindBoardDownAsync()
        {
            foreach (var value_boardname_d in _context.Displays.Where(x => x.OperationalStatus == 2).ToListAsync().Result)
            {
                var board_id = value_boardname_d.BoardID;
                var board_NAME_d = await GetBoardNameFromIDAsync(board_id);

                BoardDown_Current.Add(board_NAME_d);
                NowDownStringCollection = String.Join(",",BoardDown_Current);

            }
            foreach (var value_boardname_u in _context.Displays.Where(x => x.OperationalStatus == 1).ToListAsync().Result)
            {
                var board_id = value_boardname_u.BoardID;
                var board_NAME_u = await GetBoardNameFromIDAsync(board_id);
                BoardUp_Current.Add(board_NAME_u);
                NowUpStringCollection = String.Join(",", BoardUp_Current);
            }
        }
        /* Query the board with OP 1 (up) & 2(down) */

        /* Find boardName from ID */
        public async Task<string> GetBoardNameFromIDAsync(int boardID)
        {
            /* This function will return name of the board with input is Board's ID */
            var boardName = ""; //Define local variable
            foreach (var value_id in await _context.Boards.Where(x => x.ID == boardID).ToListAsync())
            {
                if (value_id.ID == boardID)
                {
                    boardName = value_id.Name;
                }
            }
            return boardName;
        }
        /* Find boardName from ID */

        public async Task OnGetAsync()
        {
            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");
            var day = DateTime.Now.ToString("dd");
            IQueryable<Board> boardsIQ = from b in _context.Boards
                                .Include(b => b.Zone)
                                .Include(b => b.Location)
                                .Include(b => b.Display)
                                    .ThenInclude(d => d.MessageAssignments)
                                            select b;
            boardsIQ = boardsIQ.OrderBy(b => b.ID); //Sort ID in ascending order

            Boards = await boardsIQ.AsNoTracking().ToListAsync();

            //When an IQueryable is created or modified, no query is sent to the database. 
            //The IQueryable code results in a single query that's not executed until the LAST statement.
            List<int> Board_today_ID = new List<int>();
            ReportDate = _context.ReportData.ToList();
            UpDown = _context.UptimeReports.ToList();

            foreach (var board in _context.ReportData.Where(x => x.Year == year &&
                                                            x.Months == month &&
                                                            x.Days == day).ToListAsync().Result)
            {
                Board_today_ID.Add(FindBoardID(board.Boards));
            }
            for (var id_board = 0; id_board < Board_today_ID.Count; id_board++)
            {

                var op_value = FindBoardOP(Board_today_ID[id_board]);
                if(op_value == 1)
                {
                    BoardUPTotal += 1;
                }
                else
                {
                    BoardDownTotal += 1;
                }
            }
            
            await FindBoardDownAsync();
        }



    }
}