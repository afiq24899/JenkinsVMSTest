using System;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json.Linq;
using Lingkail.VMS.Data;
using Lingkail.VMS.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net.WebSockets;

namespace Lingkail.VMS.Controllers
{
    public class TaskSchedule : ITaskSchedule
    {

        private readonly SenaVMSContext _context;
        public long up_tem { get; set; }
        public long flag_updowntime { get; set; }
        public int flag_current { get; set; }
        public bool year_check { get; set; } //false no exist in database
        public bool month_check { get; set; } //false no exist in database
        public bool day_check { get; set; } //false no exist in database
        public bool board_check { get; set; }
        public bool create_flag { get; set; } = false;
        public bool board_database { get; set; } = false; //Board already in database
        //public string[] year_total, month_total, day_total, board_total {get; set;} = new string[0];

        public TaskSchedule(SenaVMSContext context)
        {
            _context = context;
        }

        /// <summary> Assume this function always finds the correct value, later should take care the case: function return null value
        ///This function will find the board name/ID based on the IP address. 
        ///Use IP address to find the boardID in Display table then just ID to find board Name in Boards table
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public async Task<string> GetBoardIDAsync(string IP)
        {
            /* This function will return name of the board with input is Board's IP */
            var boardID = 0; //Define local variable
            var boardName = ""; //Define local variable
            foreach (var value_ip in _context.Displays.Where(x => x.C4_IP == IP).ToListAsync().Result)
            {
                if (value_ip.C4_IP == IP)
                {
                    boardID = value_ip.BoardID;
                }
            }
            foreach (var value_id in await _context.Boards.Where(x => x.ID == boardID).ToListAsync())
            {
                if (value_id.ID == boardID)
                {
                    boardName = value_id.Name;
                }
            }
            return boardName;
        }
        public string FindLocation(string nameboard)
        {   /* This function will return location of the board with input is Board's name */
            var boardID = 0; //Define local variable
            var location_board = ""; //Define local variable
            foreach (var board in _context.Boards.Where(x => x.Name == nameboard).ToListAsync().Result)
            {
                boardID = board.ID;

            }
            foreach (var s_location in _context.Locations.Where(x => x.BoardID == boardID).ToListAsync().Result)

            {
                location_board = s_location.Address;

            }
            return location_board;


        }
        public string FindStartDate(string nameboard)
        {   /* This function will return installationDate of the board with input is Board's name */
            var boardID = 0; //Define local variable
            var start_date = "1999-07-14"; //Define local variable with sample datetime
            foreach (var board in _context.Boards.Where(x => x.Name == nameboard).ToListAsync().Result)
            {
                boardID = board.ID;

            }
            foreach (var s_date in _context.Displays.Where(x => x.BoardID == boardID).ToListAsync().Result)

            {
                start_date = s_date.InstallationDate.ToString("yyyy-MM-dd");

            }
            return start_date;
        }
        public async Task UpdateEBoardsAsync(string year_u,
                                                string month_u,
                                                string day_u,
                                                string BoardName_u,
                                                int up_time_u,
                                                int down_time_u, string update_flag)
        {   /* This function will update up_time and down_time of existed Board in database */
            foreach (var timedata in await _context.ReportData.Where(x => x.Year == year_u &&
                                                            x.Months == month_u &&
                                                            x.Days == day_u &&
                                                            x.Boards == BoardName_u).ToListAsync())
            {
                /*
                 * update_flag: 0 : Update uptime
                 * update_flag: 1 : Update downtime
                 */
                if (update_flag == "0")
                {
                    timedata.UpTotal = (Int32.Parse(timedata.UpTotal) + up_time_u).ToString();
                    await _context.SaveChangesAsync();
                }
                else if (update_flag == "1")
                {
                    timedata.DownTotal = (Int32.Parse(timedata.DownTotal) + down_time_u).ToString();
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Debug.WriteLine("Please check the update_flag again");
                }



            }
        }


        public async Task CreateNewBoardAsync(string year_c,
                                                string month_c,
                                                string day_c,
                                                string BoardName_c,
                                                int up_time_c,
                                                int down_time_c)
        {
            _context.ReportData.Add(
            new ReportData
            {
                Year = year_c,
                Months = month_c,
                Days = day_c,
                Boards = BoardName_c,
                UpTotal = up_time_c.ToString(),
                DownTotal = down_time_c.ToString(),
                StartDate = FindStartDate(BoardName_c),
                Location = FindLocation(BoardName_c),
                Remark = "-"
            });
            await _context.SaveChangesAsync();

        }

        public async Task UpdateBoardsDateAsync(string BoardName, int up_time, int down_time, string update_flag)
        {

            var year = DateTime.Now.ToString("yyyy");
            var month = DateTime.Now.ToString("MM");
            var day = DateTime.Now.ToString("dd");

            foreach (var board in _context.ReportData.Where(x => x.Boards == BoardName).ToListAsync().Result)
            {

                board_database = true;
            }
            Debug.WriteLine("Update1");
            Debug.WriteLine(board_database);
            if (board_database)
            {
                Debug.WriteLine(create_flag);
                foreach (var board in _context.ReportData.Where(x => x.Year == year &&
                                                            x.Months == month &&
                                                            x.Days == day &&
                                                            x.Boards == BoardName).ToListAsync().Result)
                {
                    Debug.WriteLine("Update board");
                    if (update_flag == "0")
                    {
                        Debug.WriteLine("Update board uptime");
                    }
                    else
                    {
                        Debug.WriteLine("Update board Downtime");
                    }


                    await UpdateEBoardsAsync(year, month, day, BoardName, up_time, down_time, update_flag);
                    create_flag = true;

                }
                Debug.WriteLine(create_flag);
                if (create_flag == false)
                {
                    foreach (var board1 in await _context.ReportData.Where(x => x.Year != year &&
                                                                x.Months == month &&
                                                                x.Days == day &&
                                                                x.Boards == BoardName).ToListAsync())
                    {
                        //Debug.WriteLine("#Year: " + board1.Year);
                        //Debug.WriteLine("Create board");
                        await CreateNewBoardAsync(year, month, day, BoardName, up_time, down_time);
                        create_flag = true;
                        break;
                    }
                }
                else
                {
                }
                Debug.WriteLine(year);
                Debug.WriteLine(month);
                Debug.WriteLine(day);
                Debug.WriteLine(BoardName);
                if (create_flag == false)
                {
                    foreach (var board2 in await _context.ReportData.Where(x => x.Year == year &&
                                                                x.Months != month &&
                                                                x.Boards == BoardName).ToListAsync())
                    {
                        Debug.WriteLine("#Month: " + board2.Months);
                        Debug.WriteLine("Create board");
                        await CreateNewBoardAsync(year, month, day, BoardName, up_time, down_time);
                        create_flag = true;
                        break;
                    }
                }
                else
                {
                }
                if (create_flag == false)
                {
                    foreach (var board2 in await _context.ReportData.Where(x => x.Year == year &&
                                                                x.Months == month &&
                                                                x.Days != day &&
                                                                x.Boards == BoardName).ToListAsync())
                    {
                        Debug.WriteLine("#Day: " + board2.Days);
                        Debug.WriteLine("Create board");
                        await CreateNewBoardAsync(year, month, day, BoardName, up_time, down_time);
                        create_flag = true;
                        break;

                    }
                }
                else
                {
                }
                if (create_flag == false)
                {
                    foreach (var board2 in await _context.ReportData.Where(x => x.Year == year &&
                                                                x.Months == month &&
                                                                x.Days == day &&
                                                                x.Boards != BoardName).ToListAsync())
                    {
                        //Debug.WriteLine("#Board: " + board2.Months);
                        //Debug.WriteLine("Create board");
                        await CreateNewBoardAsync(year, month, day, BoardName, up_time, down_time);
                        create_flag = true;
                        break;
                    }
                }
                else
                {
                }

            }
            else
            {
                await CreateNewBoardAsync(year, month, day, BoardName, up_time, down_time);
                create_flag = true;
            }

        }
        public void Setdefaultvalue()
        {
            string path = @"wwwroot/ReportFile/UpTimeValue.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("");
                }
            }
            else
            {
                File.WriteAllText(path, "");
            }

            string path_time = @"wwwroot/ReportFile/StartEndDownTime.txt";
            if (!File.Exists(path_time))
            {
                using (StreamWriter sw = File.CreateText(path_time))
                {
                    sw.WriteLine("");
                }
            }
            else
            {
                File.WriteAllText(path_time, "");
            }

            string path_id = @"wwwroot/ReportFile/IdEndDownTime.txt";
            if (!File.Exists(path_time))
            {
                using (StreamWriter sw = File.CreateText(path_id))
                {
                    sw.WriteLine("");
                }
            }
            else
            {
                File.WriteAllText(path_time, "");
            }

        }
        public static void SaveIdExternalFile(int value, string board_name)
        {
            string path = @"wwwroot/ReportFile/IdEndDownTime.txt";
            var check_id = false; //Check the info of Board already in the text file or not
            var target_line = "";
            var newline = board_name + ":" + value.ToString();
            if (File.Exists(path))
            {
                string[] textLines = File.ReadAllLines(path);
                foreach (string line in textLines.Where(l => l.StartsWith(board_name)))
                {
                    check_id = true;
                    target_line = line;
                }
                if (check_id)
                {
                    UpdateSpecificLine(path, target_line, newline);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(newline);
                    }
                }


            }
            else
            {
                //Debug.WriteLine("write new file");
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("");
                }
            }

        }

        public static void SaveValueExternalFile(long value, string board_name)
        {
            string path = @"wwwroot/ReportFile/UpTimeValue.txt";
            var check_board = false; //Check the info of Board already in the text file or not
            var target_line = "";
            var newline = board_name + ":" + value.ToString();
            if (File.Exists(path))
            {
                string[] textLines = File.ReadAllLines(path);
                foreach (string line in textLines.Where(l => l.StartsWith(board_name)))
                {
                    check_board = true;
                    target_line = line;
                }
                if (check_board)
                {
                    UpdateSpecificLine(path, target_line, newline);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(newline);
                    }
                }


            }
            else
            {
                //Debug.WriteLine("write new file");
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("");
                }
            }

        }

        public static void U_FlagUpDownExternalFile(int flag, string board_name)
        {
            string path = @"wwwroot/ReportFile/StartEndDownTime.txt";
            var check_board = false; //Check the info of Board already in the text file or not
            var target_line = "";
            var newline = board_name + ":" + flag.ToString();
            if (File.Exists(path))
            {
                string[] textLines = File.ReadAllLines(path);
                foreach (string line in textLines.Where(l => l.StartsWith(board_name)))
                {
                    check_board = true;
                    target_line = line;
                }
                if (check_board)
                {
                    UpdateSpecificLine(path, target_line, newline);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(newline);
                    }
                }


            }
            else
            {
                //Debug.WriteLine("write new file");
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("");
                }
            }

        }
        public static void UpdateSpecificLine(string target_file, string targetline, string newline)
        {
            string text = File.ReadAllText(target_file);
            text = text.Replace(targetline, newline);
            File.WriteAllText(target_file, text);
        }
        public static string ReadValueExternalFile(string board_n)
        {
            var text = "";
            string[] textLines = File.ReadAllLines(@"wwwroot/ReportFile/UpTimeValue.txt");
            foreach (string line in textLines.Where(l => l.StartsWith(board_n)))
                text = line.Replace(board_n + ":", "");
            if (text != "")
            {
                return text;
            }
            else
            {
                return "0";
            }

        }

        public static string GetFlagExternalFile(string board_n)
        {
            var text = "";
            string[] textLines = File.ReadAllLines(@"wwwroot/ReportFile/StartEndDownTime.txt");
            foreach (string line in textLines.Where(l => l.StartsWith(board_n)))
                text = line.Replace(board_n + ":", "");
            if (text != "")
            {
                return text;
            }
            else
            {
                return "3";
            }

        }

        public static string GetIdExternalFile(string board_n)
        {
            var text = "";
            string[] textLines = File.ReadAllLines(@"wwwroot/ReportFile/IdEndDownTime.txt");
            foreach (string line in textLines.Where(l => l.StartsWith(board_n)))
                text = line.Replace(board_n + ":", "");
            if (text != "")
            {
                return text;
            }
            else
            {
                return "0";
            }

        }
        public async Task AddStartTimeDown(string boardname)
        {
            var star_time = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            var current_date = DateTime.Now.ToString("dd-MM-yyyy");
            _context.UptimeReports.Add(
            new UptimeReport
            {
                Name = boardname,
                Date = current_date,
                Remark = "-",
                TimeStart = star_time,
                TimeEnd = "-"
            });
            await _context.SaveChangesAsync();
            foreach (var item in _context.UptimeReports.Where(x => x.TimeStart == star_time && x.Date == current_date && x.Name == boardname).ToListAsync().Result)
            {
                SaveIdExternalFile(item.ID, boardname);
                Debug.WriteLine("ID: " + item.ID);
            }
        }

        public async Task AddEndTimeDown(string boardname)
        {
            var end_time = DateTime.Now.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            var current_date = DateTime.Now.ToString("dd-MM-yyyy");
            long idtowrite = Int64.Parse(GetIdExternalFile(boardname));
            //foreach (var item in _context.UptimeReports.Where(x => x.TimeEnd == "-" && x.Date == current_date && x.Name == boardname).ToListAsync().Result)
            //{
            //    item.TimeEnd = end_time;
            //    await _context.SaveChangesAsync();
            //}
            foreach (var item in _context.UptimeReports.Where(x => x.ID == idtowrite).ToListAsync().Result)
            {
                item.TimeEnd = end_time;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateOperationalStatus(int Operation_s, string boardip)
        {
            foreach (var item in _context.Displays.Where(x => x.C4_IP == boardip).ToListAsync().Result)
            {
                item.OperationalStatus = Operation_s;
                await _context.SaveChangesAsync();
            }

        }
        public async Task UpdateStatusBoardAsync()
        {
            string[] boardIp = new string[] { "10.20.0.200", "10.20.3.200", "121.120.145.186", "10.20.3.201", "10.20.6.61","10.20.8.10", "10.20.8.100", "121.120.145.189" };
            /*string[] boardIp = new string[] { "192.168.1.4" };*/ //For Bach's testing, comment this one when you deploy
            foreach (var board_ip in boardIp)
            {
                create_flag = false;
                var client = new RestClient("http://" + board_ip + "/api/info.json");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AlwaysMultipartFormData = true;
                //request.Timeout = 250; //ms
                IRestResponse response = client.Execute(request);
                //Debug.WriteLine(response.Content);
                //Debug.WriteLine(response.StatusCode);
                //Debug.WriteLine(board_ip);
                var board_name = GetBoardIDAsync(board_ip.ToString());
                flag_updowntime = Int64.Parse(GetFlagExternalFile(await board_name));
                if (response.StatusCode.ToString() == "OK")
                {

                    var result = JObject.Parse(response.Content);
                    var uptime_str = result["info"]["up"];
                    var uptime = Int64.Parse(uptime_str.ToString());
                    up_tem = Int64.Parse(ReadValueExternalFile(await board_name));


                    //Debug.WriteLine(up_tem);
                    if (up_tem == 0)
                    {   //Server up
                        await UpdateBoardsDateAsync(await board_name, 1, 0, "0");
                        SaveValueExternalFile(uptime, await board_name);
                        await UpdateOperationalStatus(1, board_ip); // Server up, set OP to 1
                        flag_current = 1; // Server up, current flag = 1
                        U_FlagUpDownExternalFile(1, await board_name);
                    }
                    else if (up_tem != 0 && up_tem > uptime)
                    {
                        //Server down
                        await UpdateBoardsDateAsync(await board_name, 0, 1, "1");
                        SaveValueExternalFile(uptime, await board_name);
                        await UpdateOperationalStatus(2, board_ip); // Server down, set OP to 2
                        flag_current = 0; // Server down, current flag = 0
                        U_FlagUpDownExternalFile(0, await board_name);
                    }
                    else
                    {   //Server up
                        await UpdateBoardsDateAsync(await board_name, 1, 0, "0");
                        SaveValueExternalFile(uptime, await board_name);
                        await UpdateOperationalStatus(1, board_ip); // Server up, set OP to 1
                        flag_current = 1; // Server up, current flag = 1
                        U_FlagUpDownExternalFile(1, await board_name);
                    }
                }
                else
                {
                    //Debug.WriteLine(Int32.Parse(ReadValueExternalFile(await board_name)));
                    //Server down
                    await UpdateBoardsDateAsync(await board_name, 0, 1, "1");
                    await UpdateOperationalStatus(2, board_ip); // Server up, set OP to 2
                    flag_current = 0; // Server down, current flag = 0
                    U_FlagUpDownExternalFile(0, await board_name);
                }
                Debug.WriteLine(flag_updowntime);
                Debug.WriteLine(flag_current);
                if (flag_updowntime == 3 && flag_current == 0)
                {
                    //board down, first time check
                    await AddStartTimeDown(await board_name);

                }
                else if (flag_updowntime == 3 && flag_current == 1)
                {
                    //board up, first time check

                }
                else if (flag_updowntime == 1 && flag_current == 1)
                {
                    //board up

                }
                else if (flag_updowntime == 1 && flag_current == 0)
                {
                    //start counting down time
                    await AddStartTimeDown(await board_name);
                }

                else if (flag_updowntime == 0 && flag_current == 1)
                {
                    //end counting down time
                    await AddEndTimeDown(await board_name);
                }
                else
                {
                    //flag_updowntime == 0 && flag_current == 0
                    //board down and already count
                }

            }

        }
    }
}
