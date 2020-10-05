using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Lingkail.VMS.Services;
using Lingkail.VMS.Data;
using static Lingkail.VMS.Models.Services;
using System;

namespace Lingkail.VMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private DatabaseAPIServices.ForTravelTime traveltimeService;
        private DatabaseAPIServices.ForParking parkingService;
        private DatabaseAPIServices.ForVidBroadcast videoService;
        private DatabaseAPIServices.ForWeather weatherService;


        public DatabaseController(
            DatabaseAPIServices.ForTravelTime databaseTravelTimeService,
            DatabaseAPIServices.ForParking databaseParkingService,
            DatabaseAPIServices.ForVidBroadcast databaseVideoService,
            DatabaseAPIServices.ForWeather databaseWeatherService
            )
        {
            traveltimeService = databaseTravelTimeService;
            parkingService = databaseParkingService;
            videoService = databaseVideoService;
            weatherService = databaseWeatherService;
        }


        [Route("traveltime")]
        [HttpPost]
        public ActionResult UpdateTravelTimeAsync([FromBody] TravelTimeUpdateRequest request)
        {
            Console.WriteLine("\n\n---[TRAVELTIME] ENDPOINT CALLED---");

            string userInput = string.Format(
                "\nDestinationId: {0}" +
                "\nEventType(?): {1}" +
                "\nSname: {2}" +
                "\nName: {3}" +
                "\nDatetime: {4}" +
                "\nETA: {5}",
                request.id, request.eventType, request.sname, request.name,
                request.datetime, request.eta);
            Console.WriteLine(userInput);


            //call traveltimeService to update the values into postgresql database 
            traveltimeService.updateTravelTime(request.id, request.eventType, request.sname, request.name,
                request.datetime, request.eta);

            Console.WriteLine("\nExecution OK\n");
            return Ok();
        }

        [Route("parkinginfo")]        
        [HttpPost]
        public ActionResult UpdateParkingAsync([FromBody] ParkingUpdateRequest request)
        {
            //call parkingService to update the values into postgresql database 
            parkingService.updateParking(request.id, request.MallID, request.sname, request.Bname, request.parking);        

            return Ok();
        }

        [Route("weather")]           
        [HttpPost]
        public ActionResult UpdateWeatherAsync([FromBody] WeatherRequest request)
        {
            Console.WriteLine("\n\n---[WEATHER] ENDPOINT CALLED---");

            string userInput = string.Format(
                "\nwid: {0}" +
                "\nwmorning: {1}" +
                "\nwafternoon: {2}" +
                "\nwnight: {3}" +
                "\nwdate: {4}",
                request.wid, request.wmorning, request.wafternoon,
                request.wnight, request.wdate);
            Console.WriteLine(userInput);

            if (request.wmorning == "")
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


            Console.WriteLine("\nExecution OK\n");
            return Ok();
        }

    }
}