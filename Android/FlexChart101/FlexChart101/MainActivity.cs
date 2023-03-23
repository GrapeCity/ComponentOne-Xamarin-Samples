using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace FlexChart101
{
    [Activity(Label = "FlexChart101", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ListView mSampleList;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            this.SetContentView(Resource.Layout.activity_main);

            ActionBar.Title = GetString(Resource.String.appName);

            mSampleList = (ListView)FindViewById(Resource.Id.listView1);
            SampleListAdapter adapter = new SampleListAdapter(this);
            mSampleList.Adapter=adapter;
            mSampleList.ItemClick += ItemClick;
        }
        public void ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var activityType = ((sender as ListView).GetItemAtPosition(e.Position) as SampleModel).ActivityType;
            Intent intent = new Intent(this, activityType);
            StartActivity(intent);
        }
    }
}

