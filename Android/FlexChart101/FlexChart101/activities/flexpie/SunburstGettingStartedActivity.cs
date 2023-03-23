using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using C1.Android.Chart;

namespace FlexChart101.Pie
{
    [Activity(Label = "@string/gettingStarted")]
    public class SunburstGettingStartedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            C1Sunburst sunburst = new C1Sunburst(this);
            sunburst.ItemsSource = new SunburstViewModel().FlatData;
            sunburst.Binding = "Value";
            sunburst.BindingName = "Year,Quarter,Month";
            sunburst.ToolTipContent = "{}{name}\n{y}";
            sunburst.DataLabel.Position = PieLabelPosition.Center;
            sunburst.DataLabel.Content = "{}{name}";

            LinearLayout layout = new LinearLayout(this);
            LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            layout.AddView(sunburst, param);

            SetContentView(layout);
            ActionBar.Title = GetString(Resource.String.sunburstGettingStarted);
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
