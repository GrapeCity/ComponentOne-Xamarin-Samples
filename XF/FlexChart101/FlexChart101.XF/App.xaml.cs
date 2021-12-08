using Xamarin.Forms;

namespace FlexChart101
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Xamarin.Forms.NavigationPage(new FlexChartSamples()) { BarBackgroundColor = Color.FromHex("#9D2235"), BarTextColor = Color.White };
        }
    }
}
