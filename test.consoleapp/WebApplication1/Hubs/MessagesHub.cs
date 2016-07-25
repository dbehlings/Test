using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;

namespace WebApplication1.Hubs
{
    public class SystemMessagesHub : Hub
    {
        private IHubProxy _hub;
        private static ConcurrentDictionary<string, IDisposable> subscriptions = new ConcurrentDictionary<string, IDisposable>();

        public SystemMessagesHub()
        {
            var url = "https://localhost:44309/";
            var connection = new HubConnection(url);

            _hub = connection.CreateHubProxy("HomeHub");

            //var writer = new StreamWriter(@"c:\temp\systemmessages-trace.txt");
            //writer.AutoFlush = true;

            //connection.TraceLevel = TraceLevels.All;
            //connection.TraceWriter = writer;

            connection.Start().Wait();

        }


        public override Task OnConnected()
        {
            subscriptions.TryAdd(Context.ConnectionId, Subscribe(Clients.Caller));
            return base.OnConnected();
        }

        private IDisposable Subscribe(dynamic caller)
        {
            return _hub.On<IEnumerable<object>>("NewMessage", data =>
            {
                caller.newMessage(data);
            });

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            IDisposable sub = null;
            if (subscriptions.TryRemove(Context.ConnectionId, out sub))
            {
                sub.Dispose();
            }
            return base.OnDisconnected(stopCalled);
        }






        public async Task<IEnumerable<string>> GetSources()
        {
            return await _hub.Invoke<IEnumerable<string>>("GetSources");
        }




    }
}