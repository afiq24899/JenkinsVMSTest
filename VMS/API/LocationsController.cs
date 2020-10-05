using System.Collections.Generic;
using Lingkail.VMS.Services;
using Lingkail.VMS.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// FOR TESTING PURPOSES ONLY, TO BE REMOVED IN THE FUTURE
/// HTML codes under 'old' folder > virtualBoard2.cshtml
/// </summary>

namespace Lingkail.VMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        public JsonFileLocationService locationsService { get; }

        public LocationsController(
            JsonFileLocationService jsonfilelocationservice)
        {
            locationsService = jsonfilelocationservice;
        }


        [HttpGet]
        public IEnumerable<Location> Get()
        {
            return locationsService.GetLocations();
        }

        [Route("address")]
        [HttpPatch]
        public ActionResult Patch([FromBody] AddressRequest request)
        {
            locationsService.UpdateAddress(request.BoardId, request.Address);

            return Ok();
        }

        public class AddressRequest
        {
            public int BoardId { get; set; }
            public string Address { get; set; }
        }


        /*[Route("address")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] int BoardId, 
            [FromQuery] string Address)
        {
            locationsService.UpdateAddress(BoardId, Address);

            return Ok();
        }*/

        //Test with this query string https://localhost:44321/api/locations/address?BoardId=1&Address=Test0

    }
}