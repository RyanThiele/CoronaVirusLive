using CoronaVirusLive.ViewModels;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CoronaVirusLive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        MapViewModel viewModel;

        public MapPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new MapViewModel();

            MessagingCenter.Subscribe<MapViewModel, IEnumerable<Pin>>(this, "PinsUpdated", (sender, args) =>
            {
                Map.Pins.Clear();
                if (args != null && args.Count() >= 0) args.ToList().ForEach(a => Map.Pins.Add(a));
            });
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();


        }
    }
}