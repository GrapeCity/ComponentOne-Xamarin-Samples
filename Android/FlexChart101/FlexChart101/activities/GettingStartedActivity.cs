using Android.App;
using Android.OS;
using Android.Views;
using C1.Android.Chart;
using FlexChart101.DataModel;

namespace FlexChart101
{
    [Activity(Label = "@string/gettingStarted")]
    public class GettingStartedActivity : Activity
    {
        private FlexChart mChart;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_getting_started);

            ActionBar.Title = GetString(Resource.String.gettingStarted);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            // initializing widget
            mChart = this.FindViewById<FlexChart>(Resource.Id.flexchart);
            // set the binding for X-axis of FlexChart
            
            Chartinitialization.InitialDefaultChart(mChart, ChartPoint.GetList());

            // set title/footer
            mChart.Header = "FlexChart Sales";
            mChart.Footer = "GrapeCity";
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
