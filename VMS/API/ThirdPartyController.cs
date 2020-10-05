using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lingkail.VMS.Data;
using Lingkail.VMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using RestSharp;

namespace Lingkail.VMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThirdPartyController : ControllerBase
    {
        #region API Samples
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

        private readonly SenaVMSContext _context;
        private DatabaseAPIServices.ForIncident _incidentServices;

        //Constructor 
        public ThirdPartyController(
            SenaVMSContext context,
            DatabaseAPIServices.ForIncident databaseIncidentService) 
        { 
            _context = context;
            _incidentServices = databaseIncidentService;
        }

        public class IncidentPostModel
        {
            public int IncidentTypeId { get; set; }
            public string Image { get; set; }
            public string Text { get; set; }
            public bool IsFullWebPath { get; set; }
            public int BoardId { get; set; }
            public string BoardName { get; set; }
        }

        [Route("Incident")]
        [HttpPost]
        public async Task<IActionResult> IncidentHandler([FromBody] IncidentPostModel request) 
        {
            Console.WriteLine("\n\n---[INCIDENT] ENDPOINT CALLED---");

            string userInput = string.Format(
                    "\nIncidentTypeId: {0}" +
                    "\nImageName: {1}" +
                    "\nText: {2}" +
                    "\nIsFullWebPath: {3}" +
                    "\nBoardId: {4}" +
                    "\nBoardName: {5}",
                request.IncidentTypeId, request.Image,
                request.Text, request.IsFullWebPath, request.BoardId, request.BoardName);
            Console.WriteLine(userInput);


            bool isOK = await _incidentServices.HandleNewIncident(request.IncidentTypeId, request.Image, 
                request.Text, request.IsFullWebPath, request.BoardId, request.BoardName);

            if (isOK)
            {
                Console.WriteLine("\nExecution OK\n");
                return Ok();
            }
            else 
            {
                Console.WriteLine("\nExecution NOT OK\n");
                return BadRequest("Note: Check that BoardId and BoardName are valid and corresponding to each other.");
            }
        }

    }
}
