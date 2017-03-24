using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSitemapTester.BLL.Entities
{
    public static class Connections
    {
        private static Object locker = new Object();
        private static Dictionary<string, CancellationTokenSource> tasks = new Dictionary<string, CancellationTokenSource>();

        public static void Add(string connectionId, CancellationTokenSource tokenSrc)
        {
            lock (locker)
            {
                tasks.Add(connectionId, tokenSrc);
            }
        }

        public static CancellationTokenSource GetToken(string connectionId)
        {
            lock (locker)
            {
                return tasks.Single(x => x.Key == connectionId).Value;
            }
        }

        public static void Remove(string connectionId)
        {
            lock (locker)
            {
                tasks.Remove(connectionId);
            }
        }
    }
}
