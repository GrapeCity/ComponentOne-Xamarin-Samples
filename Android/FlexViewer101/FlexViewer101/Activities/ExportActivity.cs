using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using C1.Android.Viewer;
using System.IO;

namespace FlexViewer101
{
    [Activity(Label = "ExportActivity")]
    public class ExportActivity : Activity
    {
        MemoryStream memoryStream;
        FlexViewer flexViewer;
        private string FILENAME = "ExportedPdf";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PdfBrowser);
            ActionBar.Title = GetString(Resource.String.ExportTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            // Create your application here

            flexViewer = FindViewById<FlexViewer>(Resource.Id.FlexViewer);
            flexViewer.ShowMenu = false;
            using (var stream = Assets.Open("Simple List.pdf", Android.Content.Res.Access.Streaming))
            {
                using (var sr = new StreamReader(stream))
                {
                    memoryStream = new MemoryStream();
                    sr.BaseStream.CopyTo(memoryStream);
                    flexViewer.LoadDocument(memoryStream);
                }
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var editMenuItem = menu.Add(0, 0, 0, Resource.String.PdfBrowserTitle);
            editMenuItem.SetShowAsAction(ShowAsAction.Always);
            editMenuItem.SetIcon(Resource.Drawable.ic_action_save);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 0)
            {
                Save(item.ActionView);
            }
            else if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        public void Save(View view)
        {
            var menu = new PopupMenu(this, view);
            menu.MenuItemClick += (s1, arg1) =>
            {
                string type = arg1.Item.TitleFormatted.ToString();
                string PathAndName = string.Empty;
                switch (type)
                {
                    case "pdf":
                        PathAndName = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), FILENAME) + "." + type;
                        flexViewer.Save(PathAndName);
                        menu.Dismiss();
                        break;
                    case "png":
                        PathAndName = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), FILENAME + "{0}") + "." + type;
                        flexViewer.SaveAsPng(PathAndName, GrapeCity.Documents.Common.OutputRange.All);
                        menu.Dismiss();
                        break;

                }
                if (type != "Cancel")
                {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                    alert.SetTitle(GetString(Resource.String.Saved));
                    alert.SetMessage(GetString(Resource.String.FileSavedTo) + PathAndName);
                    alert.SetPositiveButton("OK", (senderAlert, args) => { });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
            };
            menu.Inflate(Resource.Layout.PopUp);
            menu.Show();
        }
    }
}