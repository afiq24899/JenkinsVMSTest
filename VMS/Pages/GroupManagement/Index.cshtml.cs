using System.Collections.Generic;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lingkail.VMS.Pages.GroupManagement
{
    public class IndexModel : PageModel
    {
        private readonly SenaVMSContext _context;

        public IndexModel(SenaVMSContext context)
        {
            _context = context;
        }

        public IList<GroupPreset> GroupPresets { get; set; }

        public async Task OnGetAsync()
        {
            GroupPresets = await _context.GroupPresets.ToListAsync();
        }
    }
}