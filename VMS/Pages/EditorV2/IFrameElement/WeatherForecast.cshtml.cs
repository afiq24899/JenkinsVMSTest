using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lingkail.VMS.Models;
using Lingkail.VMS.Data;
using Newtonsoft.Json;
using RestSharp;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Lingkail.VMS.Pages.EditorV2.IFrameElement
{

    public class WeatherForecastModel : PageModel
    {
        private readonly SenaVMSContext _context;

        public WeatherForecastModel(SenaVMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == 0) { return new JsonResult("Not applicable for multiple editing"); }
            else
            {
                Board = await _context.Boards
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id); //Get board id from parent page (index) to this child page 
                                                       //Using string query from iframe - <iframe src="/EditorV2/Traveltime?id=x"></iframe>
                return Page();
            }
        }
    }

}