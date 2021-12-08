using Xamarin.Forms;

namespace C1Gauge101
{
    public partial class App : global::Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Xamarin.Forms.NavigationPage(new GaugeSamples()) { BarBackgroundColor = Color.FromHex("#9D2235"), BarTextColor = Color.White };
        }
    }
}
