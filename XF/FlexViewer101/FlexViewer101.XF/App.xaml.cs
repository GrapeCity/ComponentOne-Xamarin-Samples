using Xamarin.Forms;

namespace FlexViewer101
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Xamarin.Forms.NavigationPage(new FlexViewerSamples()) { BarBackgroundColor = Color.FromHex("#9D2235"), BarTextColor = Color.White };
        }
    } 
}
