using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Lingkail.VMS.Services
{
    public class DotnetHubInvoker 
    {
        private readonly IHttpContextAccessor _httpContext;

        private HubConnection hubConnection;

        public DotnetHubInvoker(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor;
        }

        #region Lingkail.VMS.Hubs namespace - BroadcastHub class 
        private async Task StartBroadcastHub() //endpoint="/broadcastHub"
        {
            string endpoint = "/broadcastHub";

            HttpRequest requestHttp = _httpContext.HttpContext.Request;
            string thisHost = string.Format("{0}://{1}",requestHttp.Scheme,requestHttp.Host);

            hubConnection = new HubConnectionBuilder()
                            .WithUrl(thisHost + endpoint)
                            .Build();

            await hubConnection.StartAsync();
        }
        public async Task GenaralBroadcast(string broadcastThis)
        {
            await StartBroadcastHub();

            await hubConnection.InvokeAsync("GenaralBroadcast",
                 broadcastThis);
        }
        public async Task UpdateProgress(int showOnBar_max100, string progressMessage)
        {
            await StartBroadcastHub();

            await hubConnection.InvokeAsync("UpdateProgress",
                 showOnBar_max100, progressMessage);
        }
        public async Task IncidentAlert(int boardId, string boardName)
        {
            await StartBroadcastHub();

            await hubConnection.InvokeAsync("IncidentAlert", boardId, boardName);
        }
        #endregion
    }
}
