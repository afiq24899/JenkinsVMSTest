using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lingkail.VMS.Models;
using Microsoft.EntityFrameworkCore;

namespace Lingkail.VMS.Pages.EditorV2.IFrameElement
{
    public class ParkingModel : PageModel
    {
        private readonly Lingkail.VMS.Data.SenaVMSContext _context;

        public ParkingModel(
            Lingkail.VMS.Data.SenaVMSContext context)
        {
            _context = context;
        }

        public List<DBKL_PGIS> ParkingInfos { get; set; }

        public void OnGet()
        {
            ParkingInfos = _context.ParkingInfos
                .AsNoTracking()
                .ToList();
        }
    }
}