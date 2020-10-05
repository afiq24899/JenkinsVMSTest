using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lingkail.VMS.Pages.ConfigurationPage
{
    public class IndexModel : PageModel
    {
        private readonly Data.SenaVMSContext _context;
        public IndexModel(Data.SenaVMSContext context)
        {
            _context = context;
        }
        [BindProperty] public Board Board { get; set; }
        [BindProperty] public Display Display { get; set; }
        [BindProperty] public Location Location { get; set; }
        public IList<Board> Boards { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Board> boardsIQ = from b in _context.Boards
                                .Include(b => b.Location)
                                .Include(b => b.Display)
                                         select b;
            boardsIQ = boardsIQ.OrderBy(b => b.ID); //Sort ID in ascending order

            Boards = await boardsIQ.AsNoTracking().ToListAsync();

        }
    }
}