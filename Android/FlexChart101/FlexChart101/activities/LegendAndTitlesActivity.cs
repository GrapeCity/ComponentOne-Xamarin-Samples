using Android.App;
using Android.OS;
using Android.Views;
using C1.Android.Chart;
using FlexChart101.DataModel;

namespace FlexChart101
{
    [Activity(Label = "@string/legendAndTitles")]
    public class LegendAndTitlesActivity : Activity
    {
        private FlexChart mChart;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_legend_and_titles);


            ActionBar.Title = GetString(Resource.String.legendAndTitles);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            // initializing widget
            mChart = this.FindViewById<FlexChart>(Resource.Id.flexchart);
            Chartinitialization.InitialDefaultChart(mChart, ChartPoint.GetList());
            mChart.ChartType = ChartType.Scatter;
            
            mChart.AxisX.Title="Country";
            mChart.AxisX.MajorGrid=true;
            mChart.AxisY.Title = "Amount";
            
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
