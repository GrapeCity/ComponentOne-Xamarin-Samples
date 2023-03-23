using Android.App;
using Android.OS;
using Android.Views;
using C1.Android.Chart;
using FlexChart101.DataModel;

namespace FlexChart101
{
    [Activity(Label = "@string/mixedChartTypes")]
    public class MixedChartTypesActivity : Activity
    {
        private FlexChart mChart;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_mixed_chart_types);

            ActionBar.Title = GetString(Resource.String.mixedChartTypes);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            // initializing widgets
            mChart = (FlexChart)FindViewById(Resource.Id.flexchart);

            Chartinitialization.InitialDefaultChart(mChart,ChartPoint.GetList());
            (mChart.Series[2] as ChartSeries).ChartType = ChartType.Line;            
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
