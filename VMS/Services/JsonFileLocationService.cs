using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using Lingkail.VMS.Models;
using Lingkail.VMS.Data;
using Microsoft.EntityFrameworkCore; //Use EF Core

namespace Lingkail.VMS.Services
{
    public class JsonFileLocationService
    {

        public JsonFileLocationService(
            IWebHostEnvironment webHostEnvironment,
            SenaVMSContext context)
        {
            WebHostEnvironment = webHostEnvironment;
            _context = context;
        }


        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly SenaVMSContext _context;
        public Board Board { get; set; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "locations.json"); }
        }

        public IEnumerable<Location> GetLocations()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Location[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void UpdateAddress(int boardID, string address)
        {
            var locations = GetLocations(); //Gets all 'locations' from json file

            Board = _context.Boards
                .Include(b => b.Location)
                .AsNoTracking()
                .FirstOrDefault(m => m.ID == boardID);  //Gets the 'location' for that specific boardId


            if (locations.First(x => x.BoardID == boardID).Address == null)
            {
                locations.First(x => x.BoardID == boardID).Address = address;
            }
            else
            {
                //overwrite the address too
                locations.First(x => x.BoardID == boardID).Address = address;

                //Save changes in the database
                Board.Location.Address = address;
                _context.Update(Board);
                _context.SaveChanges();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Location>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    locations
                );
            }
        }
    }
}
