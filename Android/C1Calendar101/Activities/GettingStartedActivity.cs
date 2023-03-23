using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace C1Calendar101
{
    [Activity(Label = "@string/getting_started", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class GettingStartedActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GettingStarted);
            ActionBar.Title = GetString(Resource.String.getting_started);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
           
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