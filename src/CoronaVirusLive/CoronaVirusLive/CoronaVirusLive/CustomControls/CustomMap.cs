using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace CoronaVirusLive.CustomControls
{
    public class CustomMap : Map
    {
        public event EventHandler CustomPinsUpdated;

        private void OnCustomPinsUpdated()
        {
            if (CustomPinsUpdated != null) CustomPinsUpdated.Invoke(this, new System.EventArgs());
        }

        private List<CustomPin> customPins;

        public List<CustomPin> CustomPins
        {
            get { return customPins; }
            set { customPins = value; OnCustomPinsUpdated(); }
        }



    }
}
