using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingkail.VMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lingkail.VMS.Models;

namespace Lingkail.VMS.Pages.VMS
{
    public class VirtualBoard2Model : PageModel
    {
        private readonly Lingkail.VMS.Data.SenaVMSContext _context;
        public JsonFileLocationService _locationService;
        public IEnumerable<Location> MyLocations { get; private set; }

        public VirtualBoard2Model(
            Lingkail.VMS.Data.SenaVMSContext context,
            JsonFileLocationService locationService)
        {
            _context = context;
            _locationService = locationService;
        }

        /*public TrafficInfo TrafficInfo { get; set; }
        public int latestTimeTravel { get; set; } //Single Digit

        public int[] latestTimeTravel2 { get; set; } //Double Digit


        //Split a number into individual digit
        int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }*/

        public void OnGet() 
        {
            MyLocations = _locationService.GetLocations();
        }



        /*public async Task OnGetAsync()
        {
            TrafficInfo = await _context.TrafficInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.ID == 1);

            string latestTimeTravelString = TrafficInfo.TimeTravel;
            latestTimeTravel = Int16.Parse(latestTimeTravelString);

            if (latestTimeTravel > 10)
            {
                latestTimeTravel2 = GetIntArray(latestTimeTravel);
            }
            else
            {
                latestTimeTravel2 = new int[2];
            }
        }*/

    }
}