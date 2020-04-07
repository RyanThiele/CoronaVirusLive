using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace CoronaVirusLive.CustomControls
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }

        public int ConfirmedChangedAmount { get; set; }
        public int DeathsChangedAmount { get; set; }
        public int RecoveredChangedAmount { get; set; }
        public DateTime ChangedAmountDateTime { get; set; }


        #region "Address

        private string cityRegion;

        public string CityRegion
        {
            get { return cityRegion; }
            set { cityRegion = value; UpdateAddress(); }
        }


        private string stateProvince;

        public string StateProvince
        {
            get { return stateProvince; }
            set { stateProvince = value; UpdateAddress(); }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; UpdateAddress(); }
        }


        private void UpdateAddress()
        {
            List<string> addressParts = new List<string>();
            if (!String.IsNullOrWhiteSpace(cityRegion)) addressParts.Add(cityRegion);
            if (!String.IsNullOrWhiteSpace(stateProvince)) addressParts.Add(stateProvince);
            if (!String.IsNullOrWhiteSpace(country)) addressParts.Add(country);

            Address = String.Join(", ", addressParts);
        }

        #endregion

    }


}
