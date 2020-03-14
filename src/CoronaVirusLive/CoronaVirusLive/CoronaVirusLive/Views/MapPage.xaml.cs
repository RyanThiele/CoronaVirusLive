using CoronaVirusLive.CustomControls;
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


                if (args != null && args.Count() >= 0)
                {
                    List<CustomPin> customPins = new List<CustomPin>();

                    foreach (Pin pin in args)
                    {
                        CustomPin customPin = new CustomPin
                        {
                            Type = PinType.Place,
                            Position = new Position(pin.Position.Latitude, pin.Position.Longitude),
                            Label = pin.Label,
                            Address = pin.Address,
                            Name = "Xamarin"
                        };

                        customMap.Pins.Add(customPin);
                        customPins.Add(customPin);
                    }

                    customMap.CustomPins = new List<CustomPin>(customPins);
                    //customMap.OnCustomPinsUpdated();
                }
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));


            });
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();


        }
    }
}