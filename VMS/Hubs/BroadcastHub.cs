using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Lingkail.VMS.Hubs
{
    public class BroadcastHub : Hub
    {
        public Task GenaralBroadcast(string message) 
        {
            return Clients.All.SendAsync("BroadcastMessage", message);
        }

        public Task UpdateProgress(int progress_max100, string progress_statement)
        {
            return Clients.All.SendAsync("BroadcastProgress", progress_max100, progress_statement);
        }

        public Task IncidentAlert(int boardId, string boardName)
        {
            return Clients.All.SendAsync("WindowAlert", boardId, boardName);
        }

    }
}
