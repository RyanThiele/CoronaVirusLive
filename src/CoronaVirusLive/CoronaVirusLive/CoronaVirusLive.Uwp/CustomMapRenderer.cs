using CoronaVirusLive.CustomControls;
using CoronaVirusLive.Uwp;
using CoronaVirusLive.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace CoronaVirusLive.Uwp
{
    public class CustomMapRenderer : MapRenderer
    {
        MapControl nativeMap;
        List<CustomPin> customPins;
        XamarinMapOverlay mapOverlay;
        bool xamarinOverlayShown = false;



        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap.MapElementClick -= OnMapElementClick;
                nativeMap.Children.Clear();
                mapOverlay = null;
                nativeMap = null;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MapControl;


                nativeMap.MapElementClick += OnMapElementClick;


                formsMap.CustomPinsUpdated += (s, eventArgs) =>
                {
                    nativeMap.Children.Clear();
                    customPins = formsMap.CustomPins;

                    foreach (var pin in customPins)
                    {
                        var snPosition = new BasicGeoposition { Latitude = pin.Position.Latitude, Longitude = pin.Position.Longitude };
                        var snPoint = new Geopoint(snPosition);

                        var mapIcon = new MapIcon();
                        mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///pin.png"));
                        mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                        mapIcon.Location = snPoint;
                        mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                        nativeMap.MapElements.Add(mapIcon);
                    }
                };



            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if (mapIcon != null)
            {
                if (xamarinOverlayShown)
                {
                    nativeMap.Children.Remove(mapOverlay);
                    xamarinOverlayShown = false;
                }
                var customPin = GetCustomPin(mapIcon.Location.Position);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                mapOverlay = new XamarinMapOverlay(customPin);


                var snPosition = new BasicGeoposition { Latitude = customPin.Position.Latitude, Longitude = customPin.Position.Longitude };
                var snPoint = new Geopoint(snPosition);

                nativeMap.Children.Add(mapOverlay);
                MapControl.SetLocation(mapOverlay, snPoint);
                MapControl.SetNormalizedAnchorPoint(mapOverlay, new Windows.Foundation.Point(0.5, 1.0));
                xamarinOverlayShown = true;
            }
        }

        CustomPin GetCustomPin(BasicGeoposition position)
        {
            var pos = new Position(position.Latitude, position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == pos)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}
