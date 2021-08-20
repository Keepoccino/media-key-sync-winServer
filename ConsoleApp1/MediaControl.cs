using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Control;

namespace ConsoleApp1
{
    class MediaControl
    {
        private GlobalSystemMediaTransportControlsSession session;

        public async Task init()
        {
            var gsmtcsm = await GetSystemMediaTransportControlsSessionManager();
            session = gsmtcsm.GetCurrentSession();
            session.MediaPropertiesChanged += Session_MediaPropertiesChanged;
        }

        private async void Session_MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        {
            var eventargs = new MediaChangeEventArgs();
            eventargs.mediaProperties = await Info();
            OnMediaInfoChange(eventargs);
        }

        protected virtual void OnMediaInfoChange(MediaChangeEventArgs e)
        {
            EventHandler<MediaChangeEventArgs> handler = MediaInfoChange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<MediaChangeEventArgs> MediaInfoChange;

        public async void Play()
        {
            await session.TryPlayAsync();
        }

        public async void Pause()
        {
            await session.TryPauseAsync();
        }

        public async void Prev()
        {
            await session.TrySkipPreviousAsync();
        }

        public async void Next()
        {
            await session.TrySkipNextAsync();
        }

        public async Task<MediaProperties> Info()
        {
            return new MediaProperties(await GetMediaProperties(session));
        }

        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
            await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        private static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>
            await session.TryGetMediaPropertiesAsync();
    }

    public class MediaChangeEventArgs : EventArgs
    {
        public MediaProperties mediaProperties { get; set; }
    }
}
