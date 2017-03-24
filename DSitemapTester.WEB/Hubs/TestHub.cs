using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DSitemapTester.Hubs
{
    public class TestHub : Hub
    {
        [HubMethodName("SendUpdateMessage")]
        public void SendUpdateMessage(string connectionId, int urlsCount)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.Client(connectionId).testFinished(urlsCount);
        }

        [HubMethodName("SendUrlsFoundedMessage")]
        public void SendUrlsFoundedMessage(string connectionId, int totalUrlsCount)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.Client(connectionId).urlsFounded(totalUrlsCount);
        }

        [HubMethodName("SendTestDoneMessage")]
        public void SendTestDoneMessage(string connectionId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.Client(connectionId).testDone();
        }
    }
}