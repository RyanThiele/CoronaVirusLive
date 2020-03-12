using CoronaVirusLive.Services;
using Xamarin.Forms;

namespace CoronaVirusLive.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        //public ICaseService CaseService => DependencyService.Get<ICaseService>();

        public MapViewModel()
        {
            Title = "Map";
        }
    }
}
