using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lingkail.VMS.Models;  //get the model(s)
using Lingkail.VMS.Data;    //get the context(s)
using Microsoft.AspNetCore.Identity;
using Lingkail.VMS.Auth.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Lingkail.VMS.Services;
using Lingkail.VMS.Models.StaticFolders;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lingkail.VMS
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Class Constructor
        //Services
        private ColorlightServices _colorlightServices;
        private CreateVsnService _createVsnService;
        private DotnetHubInvoker _dotnetHubInvoker;
        private FileManagement _fileManagement;
        //Entity Framework 
        public SenaVMSContext _context;
        public UserManager<VmsUser> _userManager;
        //Hosting Environment
        public IWebHostEnvironment _webHostEnvironment { get; set; }
        //Logger
        ILogger<IndexModel> logger;

        public IndexModel(
            ColorlightServices colorlightServices,
            CreateVsnService createVsnService,
            DotnetHubInvoker dotnetHubInvoker,
            FileManagement fileManagement,
            SenaVMSContext context,
            UserManager<VmsUser> userManager,
            IWebHostEnvironment webHostEnvironment,
            ILogger<IndexModel> logger
            )
        {
            _colorlightServices = colorlightServices;
            _createVsnService = createVsnService;
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            this.logger = logger;
            _fileManagement = fileManagement;
            _dotnetHubInvoker = dotnetHubInvoker;
        }
        #endregion

        #region Class Fields and Properties
        //Binded Properties
        [BindProperty] public DateTime?[] dateArray { get; set; }
        [BindProperty] public int?[] timerArray { get; set; }
        [BindProperty] public int?[] codeArray { get; set; }
        [BindProperty] public string[] codePlusArray { get; set; }
        [BindProperty] public string[] ImageArray { get; set; }
        [BindProperty] public string SelectedBoardIds { get; set; }
        [BindProperty] public string SelectedBoardNames { get; set; }  //View on Page only
        [BindProperty] public Board Board { get; set; }
        //AF
        [BindProperty] public string SelectedGroupPreset { get; set; }
        [BindProperty] public string SelectedPresetIds { get; set; }
        [BindProperty] public GroupPreset GroupP { get; set; }

        public IList<GroupPreset> GroupPresets { get; set; }




        //TempData
        [TempData] public string Message { get; set; }
        [TempData] public string ErrorMessage { get; set; }

        //Fields
        public string specialClass;

        public const int totalmessages = 5;
        public string fileExtension = ".jpg";
        public string rtsplink { get; set; }


        List<int> SelectedMessages = new List<int>();
        public string FailMessage = "";
        string UploadTime = "";
        List<string> Upload_OK = new List<string>();
        List<string> Upload_NOTOK = new List<string>();
        List<string> Display_OK = new List<string>();
        List<string> Display_NOTOK = new List<string>();
        #endregion

        #region Class OnGet, OnPost Methods/Handlers
        /// <summary>
        /// Page Handler (from Table Page) - post checked ids (on Multiple Edit)
        ///     a. Event handler for NO id, ONE id and MULTIPLE ids checked
        ///     b. Display all checked ids
        /// </summary>
        public async Task<IActionResult> OnPostMultipleIdAsync(string[] selectedBoards)
        {
            if (selectedBoards.Length == 0) //No checkbox selected 
            {
                return RedirectToPage("/GroupManagement/Index");
            }
            if (selectedBoards.Length == 1) //One checkbox selected
            {
                string viewSingleName = selectedBoards[0];
                SelectedBoardNames = viewSingleName;

                var thisBoardId = await _context.Boards
                    .Where(b => b.Name == viewSingleName)
                    .Select(b => b.ID)
                    .FirstOrDefaultAsync();

                await OnGetSingleIdAsync(thisBoardId, false); //Single id Handler
                return Page();
            }
            else //Many checkbox selected
            {
                //Initialize
                dateArray = new DateTime?[totalmessages];
                timerArray = new int?[totalmessages];
                codeArray = new int?[totalmessages];
                codePlusArray = new string[totalmessages];
                ImageArray = new string[totalmessages];

                string viewManyNames = string.Join(",", selectedBoards); //Convert string array (Board Name) to comma-separated string
                SelectedBoardNames = viewManyNames;

                //Get the Id with reference to the name 
                int[] idArray = new int[selectedBoards.Length];
                int i = 0;
                foreach (var boardName in selectedBoards)
                {
                    var thisBoard = await _context.Boards
                        .AsNoTracking().FirstOrDefaultAsync(m => m.Name == boardName);
                    idArray[i] = thisBoard.ID;
                    i++;
                }

                Board = new Board(); //Set to Board 0 for multiple
                SelectedBoardIds = string.Join(",", idArray); //Convert int array (Board Id) to comma-separated string
                return Page();
            }
        }



        /// <summary>
        /// Page Handler (from Table Page) - post selected id (on Edit)
        ///     a. For this id, get the latest messages from the database for preview
        /// </summary>
        public async Task<IActionResult> OnGetSingleIdAsync(int? id, bool HasAlertKey)
        {
            if (HasAlertKey) //For incoming Incident Alert
            {
                specialClass = "hidden4SpecialClass"; //manipulate css 
            }

            if (id == null) //not passed from table page
            {
                id = Board.ID; //bindproperty (hidden input), id returned from editor page
            }

            if (id == null)
            {
                return NotFound();
            }

            //Initialize
            dateArray = new DateTime?[totalmessages];
            timerArray = new int?[totalmessages];
            codeArray = new int?[totalmessages];
            codePlusArray = new string[totalmessages];
            ImageArray = new string[totalmessages];

            Board = await _context.Boards
                .Include(b => b.Display)
                .ThenInclude(d => d.MessageAssignments)
                .ThenInclude(ma => ma.EditorMessage)
                .AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            SelectedBoardNames = Board.Name;

            var allEditorMessages = await _context.EditorMessages.ToListAsync();
            var thisBoardEditorMessagesHS = new HashSet<int>(Board.Display.MessageAssignments.Select(ms => ms.EditorMessageID));
            foreach (var editorMessage in allEditorMessages)
            {
                var thisMessageId = editorMessage.ID;
                var thisBoardId = id;
                if (thisBoardEditorMessagesHS.Contains(thisMessageId)) //if message was previously selected and found 
                {
                    var thisBoardMessageAssignments = Board.Display.MessageAssignments.Where(ms => ms.EditorMessageID == thisMessageId);

                    //Note: Messages start from 1, Arrays start from 0
                    dateArray[thisMessageId - 1] = thisBoardMessageAssignments.Select(item => item.Date).FirstOrDefault();
                    timerArray[thisMessageId - 1] = thisBoardMessageAssignments.Select(item => item.Timer).FirstOrDefault();
                    codeArray[thisMessageId - 1] = thisBoardMessageAssignments.Select(item => item.EditorMessageTypeID).FirstOrDefault();
                    codePlusArray[thisMessageId - 1] = thisBoardMessageAssignments.Select(item => item.CodePlus).FirstOrDefault();
                    ImageArray[thisMessageId - 1] = thisBoardMessageAssignments.Select(item => item.ImageFileName).FirstOrDefault();
                }
                else //if message NOT previously selected 
                {
                    dateArray[thisMessageId - 1] = null;
                    timerArray[thisMessageId - 1] = 1; //1s
                    codeArray[thisMessageId - 1] = null;
                    codePlusArray[thisMessageId - 1] = null;
                    ImageArray[thisMessageId - 1] = null;
                }
            }

            //get cctv rtsplink from database according to the board selected (note : if the user select inly 1 board only.)
            int selectedboard = Board.ID;
            foreach (var test in _context.Displays.Where(x => x.BoardID == selectedboard).ToListAsync().Result)
            {
                rtsplink = test.CCTV + "#V" + test.BoardID;
            }
            return Page();
        }
        /// <summary>
        /// Page Handler (from Index Page) - submit (as final)
        ///     a. Update Group Message in the database, Note: Board 0 = Multiple
        ///     Note: Files will be saved in wwwroot/uploads - 'TempFolderSingle'/'TempFolderMultiple'
        ///     Note: Clients uploaded files will be saved and copied from 'TempFolderClient'
        ///     b. Then, in this TempFolder directory, create VSN, attempt to send program
        ///     c. If fails, throw an error message
        ///     d. If passes, copy files to respective board folder, update Message Assignment, History
        /// </summary>
        public async Task<IActionResult> OnPostUpdateAsync()
        {


            int id = Board.ID; //bindproperty (hidden input), id returned from editor page

            if (!ModelState.IsValid)
            {
                return Page();
            }

            #region Update EditorMessage; Board 0 = multiple
            for (int i = 0; i < totalmessages; i++)
            {
                int GMid = i + 1;
                var editorMessageToUpdate = await _context.EditorMessages
                    .FirstOrDefaultAsync(m => m.ID == GMid);

                editorMessageToUpdate.Date = dateArray[i];
                //If timer is null/0, set default 1s
                if ((timerArray[i] == null) || (timerArray[i] == 0))
                {
                    editorMessageToUpdate.Timer = 1; //1s
                }
                else
                {
                    editorMessageToUpdate.Timer = timerArray[i];
                }
                editorMessageToUpdate.EditorMessageTypeID = codeArray[i];
                editorMessageToUpdate.CodePlus = codePlusArray[i];
                editorMessageToUpdate.ImageFileName = editorMessageToUpdate.Message;
                editorMessageToUpdate.ThisBoard = id; // id = 0 for multiple

                _context.Update(editorMessageToUpdate);
                _context.SaveChanges();
            }
            #endregion

            #region Long process ... 
            //Get selected or valid messages first 
            int arrayIndex = 0;
            foreach (var item in codeArray)
            {
                int messageNumber = arrayIndex + 1;

                if (item != null)
                { //if type code is not null, it's a valid message
                    SelectedMessages.Add(messageNumber);
                }
                arrayIndex++;
            }
            //If no (valid) messages found 
            if (SelectedMessages.Count == 0) { return Page(); }


            //Check if it's single ids (Board x) or multiple ids (Board 0)
            if ((id != 0)) //single
            {
                try { await LongProcess(id, false, SelectedMessages); }
                catch(Exception e) {
                    var logPath = Path.Combine(_webHostEnvironment.WebRootPath,"data","TextFile.txt");
                    string str;
                    using (StreamReader sreader = new StreamReader(logPath))
                    {
                        str = sreader.ReadToEnd();
                    }
                    System.IO.File.Delete(logPath);
                    using (StreamWriter swriter = new StreamWriter(logPath, false))
                    {
                        swriter.WriteLine("--------------------------------");
                        swriter.WriteLine(DateTime.Now);
                        swriter.WriteLine(e.ToString());
                        swriter.WriteLine(str);
                    }

                    ErrorMessage = "Something went wrong! Contact maintenance team.";
                }

                if (Upload_NOTOK.Count == 0 && Display_NOTOK.Count == 0) //All OK
                {
                    return RedirectToPage("/VMS/Table");
                }
                else //NOT OK
                {
                    var faultyUpload = String.Join(",", Upload_NOTOK.ToArray());
                    var faultyDisplay = String.Join(",", Display_NOTOK.ToArray());

                    if (Upload_NOTOK.Count != 0) { FailMessage = "Error: Upload failed: (" + faultyUpload + ") @ " + UploadTime; }
                    else { FailMessage = "Error: Display failed: (" + faultyDisplay + ") @ " + UploadTime; }

                    return Page();
                }

            }
            else //mutliple
            {
                string[] selectedBoardIds_string = SelectedBoardIds.Split(',');
                int[] ids = Array.ConvertAll(selectedBoardIds_string, int.Parse);

                foreach (var thisId in ids) {
                    await LongProcess(thisId, true, SelectedMessages);
                }

                if (Upload_NOTOK.Count == 0 && Display_NOTOK.Count == 0) //All OK
                {
                    return RedirectToPage("/VMS/Table");
                }
                else //NOT OK
                {
                    var faultyUpload = String.Join(",", Upload_NOTOK.ToArray());
                    var faultyDisplay = String.Join(",", Display_NOTOK.ToArray());

                    FailMessage = "Error: Upload failed: (" + faultyUpload + "), Display failed: (" + faultyDisplay + ")  @ " + UploadTime;
                    return Page();
                }
            }
            #endregion
        }
        #endregion


        #region Class OnPost GroupManagement
        /// <summary>
        /// Page Handler (from GroupManagement Index Page) - post checked Preset Name (on Multiple Edit)
        ///     a. Event handler for NO Preset, SINGLE Preset and MULTIPLE Presets checked
        ///     b. Display Name of Preset list
        /// </summary>
        public async Task<IActionResult> OnPostMultiplePresetAsync(string[] selectedPreset) //return eg. Preset1 , Preset 2
        {

           
            if (selectedPreset.Length == 0) //No checkbox selected 
            {
                return RedirectToPage("/GroupManagement/Index");
            }
            else
            {
         
               
                dateArray = new DateTime?[totalmessages];
                timerArray = new int?[totalmessages];
                codeArray = new int?[totalmessages];
                codePlusArray = new string[totalmessages];
                ImageArray = new string[totalmessages];


                string viewManyPreset = string.Join(",", selectedPreset); //Convert string array (Preset Name) to comma-separated string
                SelectedGroupPreset = viewManyPreset;

                //Get the Id with reference to the Preset name 
                int[] idArray = new int[selectedPreset.Length];
                int i = 0;
                foreach (var PresetIDS in selectedPreset)
                {
                    var thisPreset = await _context.GroupPresets
                     .Include(m=>m.BoardGroupingAssignments)
                    .AsNoTracking().FirstOrDefaultAsync(m => m.Name == PresetIDS);
                    idArray[i] = thisPreset.ID; //return boardIDs    e.g  1,2                   
                    i++;             

                }

  

                List<int> testList = new List<int>();
                int[] ListOfBOARDID = new int[140];
                int[] ListOfID = new int[140]; //need to change 
                int d = 0;

                foreach (var ListBoardID in selectedPreset)  
                {          
          
                    foreach (var item in _context.BoardGroupingAssignments.Where(x => x.GroupPreset.ID == idArray[d]).ToArrayAsync().Result)
                    {
  
                        testList.Add(item.BoardID);
                        
                    }
                    d++;

                    ListOfID = testList.Distinct().ToArray(); //remove duplicate Board Ids in list
                    ListOfBOARDID = testList.ToArray();

                    Board = new Board(); //Set to Board 0 for multiple
                    SelectedBoardIds = string.Join(",", ListOfID); //Convert int array (Preset Name) to comma-separated string
                    //For debug
                   // SelectedGroupPreset = string.Join(",", ListOfBOARDID); //to debug what board on the list
                }
               

               
                return Page();
                //return RedirectToPage("/GroupManagement/Index");
            }
            

           
        }
        #endregion


        #region Class Methods (others)
        public async Task LongProcess(int boardId, bool isMultiple, List<int> selectedMessages) 
        {
            await _dotnetHubInvoker.UpdateProgress(0,"Start..."); //START

            // *** *** ** WRITE CODE HERE TO CHECK CONNECTION AS A PRIMARY STEP **** *** *** 
            /*
             *
             *
             *
             *
             */

            string vsnCreated = _createVsnService.createVSN(boardId, isMultiple);
            await _dotnetHubInvoker.UpdateProgress(10, "New program file created. Attempt to delete old program. Please wait..."); 

            if (await _colorlightServices.GetActiveProgramAsync(boardId) == vsnCreated) { await _colorlightServices.DeleteProgramAsync(boardId); }
            await _dotnetHubInvoker.UpdateProgress(30, "Attempt to upload and display new program. Please wait...");

            bool IsSuccessful = await _colorlightServices.SendProgramAsync(boardId, isMultiple); //Send from TempFolder
            UploadTime = DateTime.Now.ToString("ddMMMMyyyy, h:mmtt");
            await _dotnetHubInvoker.UpdateProgress(70, "Checking status. Please wait...");

            //Delay for board to play new program
            await Task.Delay(3000);
            await _dotnetHubInvoker.UpdateProgress(80, "Please wait...");

            string boardName = _context.Boards.Where(b => b.ID == boardId).Select(b => b.Name).FirstOrDefault();
            if (IsSuccessful)
            { //successfully send program  
                Upload_OK.Add(boardName);
                await _dotnetHubInvoker.GenaralBroadcast(boardName + " - Upload OK");

                if (await _colorlightServices.GetActiveProgramAsync(boardId) == vsnCreated)
                { //successfully play on display
                    await _dotnetHubInvoker.GenaralBroadcast(boardName + " - Display OK");
                    if (isMultiple)
                    {
                        _fileManagement.CopyFromTempFolderMultiple(boardId);
                    }
                    else 
                    {
                        _fileManagement.CopyFromTempFolderSingle(boardId);
                    }
                    await _dotnetHubInvoker.UpdateProgress(85, "");
                    await UpdateMessageAssignment(boardId, selectedMessages);
                    await _dotnetHubInvoker.UpdateProgress(90, ""); 
                    await UpdateHistory(boardId);
                    //---------------HERE HERE HERE-----------------------------'
                    await CombineHistory(boardId);
                    await _dotnetHubInvoker.UpdateProgress(100, ""); //END
                    Display_OK.Add(boardName);
                }
                else
                {
                    Display_NOTOK.Add(boardName);
                    await _dotnetHubInvoker.GenaralBroadcast(boardName + " - Display NOT OK");
                }
            }
            else
            {
                Upload_NOTOK.Add(boardName);
                await _dotnetHubInvoker.GenaralBroadcast(boardName + " - Upload NOT OK");
            }
        }
        public async Task UpdateHistory(int boardId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);

            //var userName = "{Public}";

            var board = await _context.Boards
                .Include(b => b.Location)
                .Include(b => b.Display)
                    .ThenInclude(d => d.MessageAssignments)
                .FirstOrDefaultAsync(m => m.ID == boardId);

            //Save images - copy from <board> folder to history folder 
            string sourcePath;
            string destPath;
            string sourceFile;
            string destFile;

            var thisBoardName = board.Name;
            var thisBoardMessages = board.Display.MessageAssignments.ToList();

            //Board Folder 
            sourcePath = Path.Combine(UploadsFolder.GetUploadsSubPath(boardId.ToString()));
            //History Folder
            string subFolderName = thisBoardName + "_" + DateTime.Now.ToString("ddMMyyyy-HHmm");
            destPath = Path.Combine(UploadsFolder.GetUploadsSubPath(UploadsFolder.History), subFolderName);
            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);

            foreach (var message in thisBoardMessages)
            {
                string fileName = message.ImageFileName + fileExtension;
                sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                destFile = System.IO.Path.Combine(destPath, fileName);
                System.IO.File.Copy(sourceFile, destFile, true);
            }

            _context.Histories.Add( //Add
                new History
                {
                    H_Name = board.Name,
                    H_Address = board.Location.Address,
                    H_NowDateTime = DateTime.Now,
                    H_User = userName,
                    Object = destPath
                });
            await _context.SaveChangesAsync();
            Message = "Successfully Edited. Please check history. Please check board.";
        }

        public async Task CombineHistory(int boardId)
        {
            var board = await _context.Boards
                .Include(b => b.Location)
                .Include(b => b.Display)
                    .ThenInclude(d => d.MessageAssignments)
                .FirstOrDefaultAsync(m => m.ID == boardId);


            string sourcePath;       
            string destComebineHistory;

            var thisBoardName = board.Name;

            //Board Folder 
            sourcePath = Path.Combine(UploadsFolder.GetUploadsSubPath(boardId.ToString()));


            //CombineHistory Folder
            string subCombineHistory = thisBoardName;
            destComebineHistory = Path.Combine(UploadsFolder.GetUploadsSubPath(UploadsFolder.CombineHistory), subCombineHistory);
            if (!Directory.Exists(destComebineHistory)) Directory.CreateDirectory(destComebineHistory);

            Bitmap newImg = new Bitmap(790, 1024);

            Graphics g = Graphics.FromImage(newImg);




            
                 g.DrawImage(Image.FromFile(Path.Combine(sourcePath,"message1.jpg")), new Point(10, 10));
                 newImg.Save(Path.Combine(destComebineHistory,"output.jpg"), ImageFormat.Jpeg);

                 g.DrawImage(Image.FromFile(Path.Combine(sourcePath, "message2.jpg")), new Point(10, 200));
                 newImg.Save(Path.Combine(destComebineHistory, "output.jpg"), ImageFormat.Jpeg);

                 g.DrawImage(Image.FromFile(Path.Combine(sourcePath, "message3.jpg")), new Point(10, 400));
                 newImg.Save(Path.Combine(destComebineHistory, "output.jpg"), ImageFormat.Jpeg);

                 g.DrawImage(Image.FromFile(Path.Combine(sourcePath, "message4.jpg")), new Point(10, 600));
                 newImg.Save(Path.Combine(destComebineHistory, "output.jpg"), ImageFormat.Jpeg);

                 g.DrawImage(Image.FromFile(Path.Combine(sourcePath, "message5.jpg")), new Point(10, 800));
                 newImg.Save(Path.Combine(destComebineHistory, "output.jpg"), ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot\uploads\" + boardID + "\\message1.jpg"), new Point(10, 10));
            //newImg.Save(@"wwwroot\uploads\CombineHistory\" + subCombineHistory + "\\output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot\uploads\" + boardID + "\\message2.jpg"), new Point(10, 200));
            //newImg.Save(@"wwwroot\uploads\CombineHistory\" + subCombineHistory + "\\output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot\uploads\" + boardID + "\\message3.jpg"), new Point(10, 400));
            //newImg.Save(@"wwwroot\uploads\CombineHistory\" + subCombineHistory + "\\output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot\uploads\" + boardID + "\\message4.jpg"), new Point(10, 600));
            //newImg.Save(@"wwwroot\uploads\CombineHistory\" + subCombineHistory + "\\output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot\uploads\" + boardID + "\\message5.jpg"), new Point(10, 800));
            //newImg.Save(@"wwwroot\uploads\CombineHistory\" + subCombineHistory + "\\output.jpg", ImageFormat.Jpeg);

            ///////////
            //g.DrawImage(Image.FromFile(@"wwwroot/uploads/" + boardID + "/message1.jpg"), new Point(10, 10));
            //newImg.Save(@"wwwroot/uploads/CombineHistory/" + subCombineHistory + "/output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot/uploads/" + boardID + "/message2.jpg"), new Point(10, 200));
            //newImg.Save(@"wwwroot/uploads/CombineHistory/" + subCombineHistory + "/output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot/uploads/" + boardID + "/message3.jpg"), new Point(10, 400));
            //newImg.Save(@"wwwroot/uploads/CombineHistory/" + subCombineHistory + "/output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot/uploads/" + boardID + "/message4.jpg"), new Point(10, 600));
            //newImg.Save(@"wwwroot/uploads/CombineHistory/" + subCombineHistory + "/output.jpg", ImageFormat.Jpeg);

            //g.DrawImage(Image.FromFile(@"wwwroot/uploads/" + boardID + "/message5.jpg"), new Point(10, 800));
            //newImg.Save(@"wwwroot/uploads/CombineHistory/" + subCombineHistory + "/output.jpg", ImageFormat.Jpeg);

        }

        public async Task UpdateMessageAssignment(int boardId, List<int> selectedMessages)
        {
            var allEditorMessages = await _context.EditorMessages.ToListAsync();
            var boardToUpdate = _context.Boards
                .Include(b => b.Location)
                .Include(b => b.Display)
                .ThenInclude(d => d.MessageAssignments)
                .ThenInclude(ma => ma.EditorMessage)
                .FirstOrDefault(d => d.ID == boardId); //current board id

            var newSelectedMessagesHS = new HashSet<int>(selectedMessages);
            var oldSelectedMessagesHS = new HashSet<int>(boardToUpdate.Display.MessageAssignments.Select(ms => ms.EditorMessageID));

            foreach (var editorMessage in allEditorMessages)
            {
                if (newSelectedMessagesHS.Contains(editorMessage.ID)) //if message is selected  
                {
                    if (!oldSelectedMessagesHS.Contains(editorMessage.ID)) //if new and non-existing in the MessageAssignment Table 
                    {
                        boardToUpdate.Display.MessageAssignments.Add( //Add
                            new MessageAssignment
                            {
                                //Just Copy from EditorMessage
                                DisplayBoardID = boardToUpdate.Display.BoardID,
                                EditorMessageID = editorMessage.ID,
                                ImageFileName = editorMessage.ImageFileName,
                                Date = editorMessage.Date,
                                Timer = editorMessage.Timer,
                                EditorMessageTypeID = editorMessage.EditorMessageTypeID,
                                CodePlus = editorMessage.CodePlus
                            });
                    }
                    else //if exist in the MessageAssignment Table 
                    {
                        var messageToUpdate = boardToUpdate.Display.MessageAssignments //Update
                            .FirstOrDefault(ma => ma.EditorMessageID == editorMessage.ID);
                        //Just Copy from EditorMessage
                        messageToUpdate.ImageFileName = editorMessage.ImageFileName;
                        messageToUpdate.Date = editorMessage.Date;
                        messageToUpdate.Timer = editorMessage.Timer;
                        messageToUpdate.EditorMessageTypeID = editorMessage.EditorMessageTypeID;
                        messageToUpdate.CodePlus = editorMessage.CodePlus;

                        _context.Update(messageToUpdate);
                    }
                }
                else //if message NOT selected 
                {
                    if (oldSelectedMessagesHS.Contains(editorMessage.ID)) //if exists in the MessageAssignment Table 
                    {
                        var editorMessageToRemove = boardToUpdate.Display.MessageAssignments //Remove
                                .SingleOrDefault(b => b.EditorMessageID == editorMessage.ID);

                        _context.Remove(editorMessageToRemove);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
    

