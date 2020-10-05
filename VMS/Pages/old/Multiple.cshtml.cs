using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore; //Use EF Core
using Lingkail.VMS.Models;

using RestSharp;//http API

using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Lingkail.VMS.Pages.VMS
{
    [Authorize]
    public class MultipleModel : PageModel
    {

        private readonly UserManager<VmsUser> _userManager;
        private readonly SignInManager<VmsUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly Lingkail.VMS.Data.SenaVMSContext _context;

        public MultipleModel(
            UserManager<VmsUser> userManager,
            SignInManager<VmsUser> signInManager,
            IEmailSender emailSender,
            Lingkail.VMS.Data.SenaVMSContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        //Upon successful editing and saving, message will pop up to notify- 'please check history'
        [TempData]
        public string Message { get; set; }


        [BindProperty]
        public Board Board { get; set; }
        [BindProperty]
        public Display Display { get; set; }
        [BindProperty]
        public History History { get; set; }
        [BindProperty]
        public IList<EditorMessage> EditorMessages { get; set; }

        [BindProperty]
        public string SelectedBoardIds { get; set; }     //"1,2..."
        [BindProperty]
        public string SelectedBoardNames { get; set; }  //"VZ101,VZ102..."


        /*OnPostCheckbox is mainly for passing selected boards (id/name) 
         * for text display and naming of images or messages in wwwroot*/
        public void OnPostCheckbox(string[] selectedBoards)
        {
            var viewNames = string.Join(",", selectedBoards);  //Convert string array (Board Name) to comma-separated string
            SelectedBoardNames = viewNames;

            string[] tempArray = new string[selectedBoards.Length]; //Initialise
            int i = 0;

            foreach (var item in selectedBoards) // Get the Id with ref to the name and populate the temparray
            {
                Board = _context.Boards
                .AsNoTracking()
                .FirstOrDefault(m => m.Name == item);

                tempArray[i] = Board.ID.ToString();
                i++;
            }

            var viewIds = string.Join(",", tempArray);  //Convert string array (Board Id) to comma-separated string
            SelectedBoardIds = viewIds;

            EditorMessages = _context.EditorMessages
                .AsNoTracking()
                .ToList();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int[] selectedMessageIds)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //1. Check selected Boards ; 2. Check selected messages 
            string[] selectedBoardIds = SelectedBoardIds.Split(',');
            var allEditorMessages = _context.EditorMessages;

            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);

            foreach (var boardId in selectedBoardIds) //Selected Boards
            {
                int boardIdInt = Int16.Parse(boardId);
                var boardDisplayToUpdate = await _context.Displays
                    .Include(d => d.MessageAssignments)
                    .ThenInclude(ma => ma.EditorMessage)
                    .FirstOrDefaultAsync(d => d.BoardID == boardIdInt);

                //Try updating with posted values
                if (await TryUpdateModelAsync<Display>(
                boardDisplayToUpdate,
                "Display",
                d => d.Screenshot_URL))
                {
                    var selectedMessagesHS = new HashSet<int>(selectedMessageIds);
                    var boardMessages = new HashSet<int>(boardDisplayToUpdate.MessageAssignments.Select(ms => ms.EditorMessageID));
                    foreach (var editorMessage in allEditorMessages)
                    {
                        if (selectedMessageIds.Contains(editorMessage.ID)) //if message selected and found 
                        {
                            if (!boardMessages.Contains(editorMessage.ID)) //if new and non-existing in the MessageAssignment Table 
                            {
                                boardDisplayToUpdate.MessageAssignments.Add( //Add
                                    new MessageAssignment
                                    {
                                        DisplayBoardID = boardDisplayToUpdate.BoardID,
                                        EditorMessageID = editorMessage.ID
                                    });
                            }
                        }
                        else //if message NOT selected 
                        {
                            if (boardMessages.Contains(editorMessage.ID)) //if exists in the MessageAssignment Table 
                            {
                                MessageAssignment editorMessageToRemove //Remove
                                    = boardDisplayToUpdate
                                        .MessageAssignments
                                        .SingleOrDefault(b => b.EditorMessageID == editorMessage.ID);
                                _context.Remove(editorMessageToRemove);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }

            //Save in History
            _context.Histories.Add( //Add
                new History
                {
                    H_Name = SelectedBoardNames,
                    H_Address = "[ADDRESS]",
                    H_NowDateTime = DateTime.Now,
                    H_User = userName
                });
            await _context.SaveChangesAsync();
            Message = "Successfully Edited. Please check history.";

            return RedirectToPage("/VMS/Table"); ;
        }

        /*
         this section is for testing to do API call via colourlight 1.6.3 to manage 
         VMS board via C4
         */

/*        public IActionResult OnPostTestAddVSN()
        {
            var client = new RestClient("http://192.168.43.65/api/program/one_pic.vsn");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data; boundary=--------------------------673427185943750020600241");
            request.AddFile("f1", "1234.jpg");
            request.AddFile("f2", "one_pic.vsn");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return RedirectToPage("/VMS/Table");
        }

        public IActionResult OnPostTestDeleteVSN()
        {
            var client = new RestClient("http://192.168.43.65/api/vsns/sources/lan/vsns/one_pic.vsn");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "text/plain");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            return RedirectToPage("/VMS/Table");
        }*/
    }
}


