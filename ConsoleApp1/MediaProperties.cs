using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Control;
using Windows.Storage.Streams;

namespace ConsoleApp1
{
    public class MediaProperties
    {
        public string title { get; private set; }
        public string artist { get; private set; }
        public string thumbnailBase64Png { get; private set; }

        public MediaProperties(GlobalSystemMediaTransportControlsSessionMediaProperties properties)
        {
            title = properties.Title;
            artist = properties.Artist;
            Task.Run(async () => thumbnailBase64Png = await convertThumbnailToBase64(properties.Thumbnail)).Wait();
        }

        private async Task<string> convertThumbnailToBase64(IRandomAccessStreamReference rawthumbnail)
        {
            if(rawthumbnail is null)
            {
                return "";
            }

            var thumbnailStream = await rawthumbnail.OpenReadAsync();
            using (MemoryStream m = new MemoryStream())
            {
                var readstream = thumbnailStream.AsStreamForRead();
                readstream.Seek(0, SeekOrigin.Begin);
                readstream.CopyTo(m);

                return Convert.ToBase64String(m.ToArray());
            }
        }
    }
}
