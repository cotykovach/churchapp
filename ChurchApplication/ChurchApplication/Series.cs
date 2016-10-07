using Android.Graphics;
using System.Net;

namespace ChurchApplication
{
    class Series
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImageBase64 { get; set; }
        public int hideTitle { get; set; }
        
        public Bitmap Image
        {
            get
            {
                if (ImageBase64 != "" && ImageBase64 != null)
                {

                    Bitmap imageBitmap = null;

                    using (var webClient = new WebClient())
                    {
                        var imageBytes = webClient.DownloadData(ImageBase64.Replace("\\", string.Empty));
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                        }
                    }

                    return imageBitmap;
                }
                return null;
            }
        }
    }
}