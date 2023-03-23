using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using C1.Android.Viewer;
using System.IO;

namespace FlexViewer101
{
    [Activity(Label = "@string/GettingStartedTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class GettingStartedActivity : Activity
    {
        MemoryStream memoryStream;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);
            ActionBar.Title = GetString(Resource.String.GettingStartedTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var flexViewer = FindViewById<FlexViewer>(Resource.Id.FlexViewer);
            flexViewer.ShowMenu = false;
            using (var stream = Assets.Open("DefaultDocument.pdf", Android.Content.Res.Access.Streaming))
            {
                using (var sr = new StreamReader(stream))
                {
                    memoryStream = new MemoryStream();
                    sr.BaseStream.CopyTo(memoryStream);
                    flexViewer.LoadDocument(memoryStream);
                }
            }
        }

        public override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            if (memoryStream != null)
            {
                memoryStream.Dispose();
                memoryStream = null;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (memoryStream != null)
            {
                memoryStream.Dispose();
                memoryStream = null;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}