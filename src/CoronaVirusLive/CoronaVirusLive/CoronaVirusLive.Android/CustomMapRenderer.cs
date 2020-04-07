using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using CoronaVirusLive.CustomControls;
using CoronaVirusLive.Droid;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace CoronaVirusLive.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<CustomPin> customPins;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                formsMap.CustomPinsUpdated += (sender, eventArgs) =>
                {
                    customPins = formsMap.CustomPins;
                };
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            //marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Id.info));
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }

            //if (!string.IsNullOrWhiteSpace(customPin.Url))
            //{
            //    var url = Android.Net.Uri.Parse(customPin.Url);
            //    var intent = new Intent(Intent.ActionView, url);
            //    intent.AddFlags(ActivityFlags.NewTask);
            //    Android.App.Application.Context.StartActivity(intent);
            //}
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);


                TextView confirmedTextWindow = view.FindViewById<TextView>(Resource.Id.ConfirmedTextWindow);
                TextView confirmedChangedAmountTextWindow = view.FindViewById<TextView>(Resource.Id.ConfirmedChangedAmountTextWindow);
                ImageView confirmedImageView = view.FindViewById<ImageView>(Resource.Id.ConfirmedImageWindow);

                if (confirmedTextWindow != null) confirmedTextWindow.Text = customPin.Confirmed.ToString();
                if (confirmedChangedAmountTextWindow != null) SetChangedAmountText(customPin.ConfirmedChangedAmount, confirmedChangedAmountTextWindow);
                if (confirmedImageView != null) SetImage(customPin.ConfirmedChangedAmount, confirmedImageView);


                TextView deathsTextWindow = view.FindViewById<TextView>(Resource.Id.DeathsTextWindow);
                TextView deathsChangedAmountTextWindow = view.FindViewById<TextView>(Resource.Id.DeathsChangedAmountTextWindow);
                ImageView deathsImageView = view.FindViewById<ImageView>(Resource.Id.DeathsImageWindow);

                if (deathsTextWindow != null) deathsTextWindow.Text = customPin.Deaths.ToString();
                if (deathsChangedAmountTextWindow != null) SetChangedAmountText(customPin.DeathsChangedAmount, deathsChangedAmountTextWindow);
                if (deathsImageView != null) SetImage(customPin.DeathsChangedAmount, deathsImageView);


                TextView recoveredTextWindow = view.FindViewById<TextView>(Resource.Id.RecoveredTextWindow);
                TextView recoveredChangesAmountTextWindow = view.FindViewById<TextView>(Resource.Id.RecoveredChangedAmountTextWindow);
                ImageView recoveredImageView = view.FindViewById<ImageView>(Resource.Id.RecoveredImageWindow);

                if (recoveredTextWindow != null) recoveredTextWindow.Text = customPin.Recovered.ToString();
                if (recoveredChangesAmountTextWindow != null) SetChangedAmountText(customPin.RecoveredChangedAmount, recoveredChangesAmountTextWindow);
                if (recoveredImageView != null) SetImage(customPin.RecoveredChangedAmount, recoveredImageView);

                TextView addressTextView = view.FindViewById<TextView>(Resource.Id.AddressTextWindow);
                if (addressTextView != null) addressTextView.Text = customPin.Address;

                return view;
            }
            return null;
        }


        private void SetImage(int amount, ImageView imageView)
        {
            if (imageView == null) return;

            if (amount == 0)
                imageView.Visibility = Android.Views.ViewStates.Gone;
            else if (amount > 0)
            {
                imageView.Visibility = Android.Views.ViewStates.Visible;
                imageView.SetImageResource(2131165327);
            }
            else
            {
                imageView.Visibility = Android.Views.ViewStates.Visible;
                imageView.SetImageResource(2131165302);
            }
        }

        private void SetChangedAmountText(int amount, TextView textView)
        {
            if (textView == null) return;

            if (amount == 0)
                textView.Text = " | +-0";
            else if (amount > 0)
                textView.Text = $" | +{amount}";
            else
                textView.Text = $" | -{amount}";
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }
    }
}

