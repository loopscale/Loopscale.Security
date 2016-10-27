using System;
using System.Drawing;
using System.IO;

namespace Loopscale.Shared.Helpers
{
    public static class ImageHelper
    {
        public static string SaveImageFromStream(byte [] imageData)
        {
            var path = ConfigHelper._Instance.SharedResourcePath;
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image image = Image.FromStream(ms);
                var imageName = Guid.NewGuid().ToString("d") + ".jpg";
                image.Save(path + imageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                return imageName;
            }
        }

        public static string GetBas364Image(string imageFile)
        {
            if (!string.IsNullOrEmpty(imageFile))
            {
                var path = ConfigHelper._Instance.SharedResourcePath + imageFile;
                return Convert.ToBase64String(File.ReadAllBytes(path));
            }
            return null;
        }
    }
}
