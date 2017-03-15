using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DSitemapTester.Hubs
{
    public class TestHub : Hub
    {
        [HubMethodName("sendMessages")]

        public static void SendUpdateMessage()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.All.addMessage();
        }
    }
}