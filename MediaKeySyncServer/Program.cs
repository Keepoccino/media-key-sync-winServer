using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Control;

namespace Server
{
    class Program
    {
        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting server..");
            var server = new MediaSyncWebsocketServer();
            server.StartServer();

            waitHandle.WaitOne();

            Console.WriteLine("Server stopped..");
        }
    }
}
