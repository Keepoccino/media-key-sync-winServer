using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Control;

namespace ConsoleApp1
{
    class Program
    {
        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        public static async Task Main(string[] args)
        {
            var server = new MediaSyncWebsocketServer();
            server.StartServer();

            waitHandle.WaitOne();

            Console.WriteLine("Press any key to quit..");
            Console.ReadKey(true);
        }

        //public static async Task Main(string[] args)
        //{
        //    var gsmtcsm = await GetSystemMediaTransportControlsSessionManager();
        //    var session = gsmtcsm.GetCurrentSession();
        //    var mediaProperties = await GetMediaProperties(session);

        //    gsmtcsm.CurrentSessionChanged += Gsmtcsm_CurrentSessionChanged;
        //    gsmtcsm.SessionsChanged += Gsmtcsm_SessionsChanged;

        //    session.MediaPropertiesChanged += Session_MediaPropertiesChanged;
        //    session.PlaybackInfoChanged += Session_PlaybackInfoChanged;

        //    Console.WriteLine("{0} - {1}", mediaProperties.Artist, mediaProperties.Title);

        //    var t = await mediaProperties.Thumbnail.OpenReadAsync();
        //    Console.WriteLine(t.ContentType);
        //    using (var fileStream = File.Create("test.png"))
        //    {
        //        var stream = t.AsStreamForRead();
        //        stream.Seek(0, SeekOrigin.Begin);
        //        stream.CopyTo(fileStream);
        //    }


        //    waitHandle.WaitOne();

        //    Console.WriteLine("Press any key to quit..");
        //    Console.ReadKey(true);
        //}

        private static void Session_PlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        {
            Console.WriteLine("Session_PlaybackInfoChanged");
        }

        private static void Session_MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        {
            Console.WriteLine("Session_MediaPropertiesChanged");
        }

        private static void Gsmtcsm_SessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender, SessionsChangedEventArgs args)
        {
            Console.WriteLine("Gsmtcsm_SessionsChanged");
        }

        private static void Gsmtcsm_CurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
        {
            Console.WriteLine("Gsmtcsm_CurrentSessionChanged");
        }

        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        private static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>
            await session.TryGetMediaPropertiesAsync();
    }
}
