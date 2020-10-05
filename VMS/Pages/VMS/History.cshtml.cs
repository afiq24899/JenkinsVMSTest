using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; //Use EF Core
using Lingkail.VMS.Models;

namespace Lingkail.VMS.Pages.VMS
{
    public class HistoryModel : PageModel
    {
        private readonly Lingkail.VMS.Data.SenaVMSContext _context;
        public HistoryModel(Lingkail.VMS.Data.SenaVMSContext context)
        {
            _context = context;
        }

        public IList<History> Histories { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<History> historiesIQ = from h in _context.Histories
                                         select h;
            historiesIQ = historiesIQ.OrderByDescending(b => b.H_NowDateTime); //Sort by last edited

            Histories = await historiesIQ.AsNoTracking().ToListAsync();
            //When an IQueryable is created or modified, no query is sent to the database. 
            //The IQueryable code results in a single query that's not executed until the LAST statement.
        }
    }
}