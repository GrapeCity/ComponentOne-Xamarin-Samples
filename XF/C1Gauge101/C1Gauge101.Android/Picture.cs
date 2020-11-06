using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Java.IO;
using Android.OS;
using Android.Provider;
using Android.Content;
using Android.Net;

[assembly: Xamarin.Forms.Dependency(typeof(C1Gauge101.Picture_Droid))]

namespace C1Gauge101
{
    public class Picture_Droid : IPicture
    {
        async public void SavePictureToDisk(string filename, Task<byte[]> imageData)
        {
            var dir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim);
            var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = filename + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string filePath = System.IO.Path.Combine(pictures, name);
            try
            {
                System.IO.File.WriteAllBytes(filePath, await imageData);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Uri.FromFile(new File(filePath)));
                Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }
    }
}