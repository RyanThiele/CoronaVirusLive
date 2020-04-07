using CoronaVirusLive.CustomControls;
using CoronaVirusLive.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CoronaVirusLive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentView
    {

        MapViewModel viewModel;

        public MapView()
        {
            InitializeComponent();

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(39.828175, -98.5795), Distance.FromMiles(1200.0)));

            BindingContext = viewModel = new MapViewModel();

            MessagingCenter.Subscribe<MapViewModel, IEnumerable<CustomPin>>(this, "PinsUpdated", async (sender, args) =>
            {

                MessagingCenter.Send<MapViewModel, string>(this.viewModel, "CasesDataStatus", "Plotting cases...");

                if (args != null && args.Count() >= 0)
                {
                    foreach (CustomPin pin in args)
                    {
                        customMap.Pins.Add(pin);
                    }

                    customMap.CustomPins = args.ToList();
                }

                MessagingCenter.Send<MapViewModel, string>(this.viewModel, "CasesDataStatus", null);
                await MoveMapToLocationAsync();

            });
        }


        private async Task MoveMapToLocationAsync()
        {

            try
            {
                Location location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));
                if (location != null)
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(40.0)));
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

    }
}