using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class MediaSyncWebsocketServer
    {
        private MediaControl control;
        private ManualResetEvent waitHandle = new ManualResetEvent(false);
        private List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();

        public async void StartServer()
        {
            control = new MediaControl();
            await control.init();

            control.MediaInfoChange += Control_MediaInfoChange;


            var server = new WebSocketServer("ws://0.0.0.0:5000");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    HandleWsMessage(message, socket);

                };
            });
        }

        private void Control_MediaInfoChange(object sender, MediaChangeEventArgs e)
        {
            allSockets.ForEach(socket => socket.Send(CreateMediaInfoMessageResponse(e.mediaProperties)));
        }

        private async void HandleWsMessage(string message, IWebSocketConnection socket)
        {
            switch (message.ToLower())
            {
                case "play":
                    control.Play();
                    break;
                case "pause":
                    control.Pause();
                    break;
                case "prev":
                    control.Prev();
                    break;
                case "next":
                    control.Next();
                    break;
                case "info":
                    await socket.Send(CreateMediaInfoMessageResponse(await control.Info()));
                    break;
                default:
                    await socket.Send(CreateErrorMessageResponse("unknown_command"));
                    break;
            }
        }

        private string CreateErrorMessageResponse(string message)
        {
            return JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        { "type", "error" },
                        { "message", "message" }
                    });
        }

        private string CreateMediaInfoMessageResponse(MediaProperties info)
        {
            return JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { "type", "mediainfo" },
                        { "info", info }
                    });
        }
    }
}
