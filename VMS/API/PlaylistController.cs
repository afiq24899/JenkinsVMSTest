using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Lingkail.VMS.Services;
using Lingkail.VMS.Data;
using static Lingkail.VMS.Models.Services;

namespace Lingkail.VMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;
        public DatabaseAPIServices.ForTravelTime traveltimeService;
        public DatabaseAPIServices.ForParking parkingService;
        public DatabaseAPIServices.ForVidBroadcast videoService;
        public DatabaseAPIServices.ForWeather weatherService;
        private readonly SenaVMSContext _context;

        private ColorlightServices _colorlightServices;
        private CreateVsnService _createVsnService;


        public PlaylistController(
            IWebHostEnvironment environment,
            DatabaseAPIServices.ForTravelTime databaseTravelTimeService,
            DatabaseAPIServices.ForParking databaseParkingService,
            DatabaseAPIServices.ForVidBroadcast databaseVideoService,
            DatabaseAPIServices.ForWeather databaseWeatherService,
            ColorlightServices colorlightServices,
            CreateVsnService createVsnService,
            SenaVMSContext context)
        {
            _hostingEnvironment = environment;
            traveltimeService = databaseTravelTimeService;
            parkingService = databaseParkingService;
            videoService = databaseVideoService;
            weatherService = databaseWeatherService;
            _colorlightServices = colorlightServices;
            _createVsnService = createVsnService;
            _context = context;
        }


        [Route("traveltime")]
        [HttpPost]
        public async Task<ActionResult> UpdateTravelTimeAsync([FromBody] TravelTimeUpdateRequest request)
        {
            //call traveltimeService to update the values into postgresql database 
            traveltimeService.updateTravelTime(request.id, request.eventType, request.sname, request.name,
                request.datetime, request.eta);

            //call createvsn to create the vsn file
            _createVsnService.createVSN(request.id, false);

            //update physical board
            await _colorlightServices.SendProgramAsync(request.id, false);

            return Ok();
        }

        [Route("parkinginfo")]             // api route for parking info (DBKL_PGIS table)
        [HttpPost]
        public async Task<ActionResult> UpdateParkingAsync([FromBody] ParkingUpdateRequest request)
        {
            //call parkingService to update the values into postgresql database 
            parkingService.updateParking(request.id, request.MallID, request.sname, request.Bname, request.parking);            // updating the parking slot in database by Jahari's side

            //call createvsn to create the vsn file
            _createVsnService.createVSN(request.id, false);

            //update physical board
            await _colorlightServices.SendProgramAsync(request.id, false);

            return Ok();
        }

        [Route("video")]             // api route for Message video (Video table)
        [HttpPost]
        public ActionResult UpdateMessage([FromBody] VideoUpdateRequest request)
        {
            //call videoService to update the values into postgresql database 
            videoService.updateMessage(request.id, request.VidType, request.Bname, request.BoardID, request.Message);    // update message video for certain event 

            //call createvsn to create the vsn file
            _createVsnService.createVSN(request.id, false);

            return Ok();
        }

        [Route("weather")]             // api route for Message Weather
        [HttpPost]
        public ActionResult UpdateWeather([FromBody] WeatherRequest request)
        {

            if(request.wmorning == "")
            {
                request.wmorning = "-";
            }
            else if (request.wafternoon == "")
            {
                request.wafternoon = "-";
            }
            else if (request.wnight == "")
            {
                request.wnight = "-";
            }
            //call weatherService to update the values into postgresql database 
            weatherService.updateWeather(request.wid, request.wmorning, request.wafternoon,
                request.wnight, request.wdate);

            return Ok();
        }

    }
}