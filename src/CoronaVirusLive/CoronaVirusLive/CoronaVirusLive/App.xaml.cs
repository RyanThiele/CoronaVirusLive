using CoronaVirusLive.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace CoronaVirusLive
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ICaseService, JohnHopkinsCaseService>();

            MainPage = new Views.MainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=d906e385-d364-4953-8ed9-77ff0ab5e47f;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
