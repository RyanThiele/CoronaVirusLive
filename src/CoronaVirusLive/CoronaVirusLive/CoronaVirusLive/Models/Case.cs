using System;
using System.Globalization;

namespace CoronaVirusLive.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string ProvinceState { get; set; }
        public string CountryRegion { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        #region Constructors

        public Case()
        {

        }

        public Case(int id, string line)
        {
            if (String.IsNullOrWhiteSpace(line)) return;
            string[] columns = line.Split(',');

            // variables
            string lastUpdatedDateTimeValue = null;
            DateTime lastUpdateDateTime = new DateTime();
            int confirmed = 0, deaths = 0, recovered = 0;
            double latitude = 0, longitude = 0;


            if (columns.Length > 0) ProvinceState = columns[0];
            if (columns.Length > 1) CountryRegion = columns[1];
            if (columns.Length > 2) lastUpdatedDateTimeValue = columns[2];
            if (columns.Length > 3) int.TryParse(columns[3], out confirmed);
            if (columns.Length > 4) int.TryParse(columns[4], out deaths);
            if (columns.Length > 5) int.TryParse(columns[5], out recovered);
            if (columns.Length > 6) double.TryParse(columns[6], out latitude);
            if (columns.Length > 7) double.TryParse(columns[7], out longitude);


            DateTime.TryParse(lastUpdatedDateTimeValue, out lastUpdateDateTime);
            if (lastUpdateDateTime == new DateTime()) DateTime.TryParseExact(lastUpdatedDateTimeValue, "M/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out lastUpdateDateTime);


            Id = id;
            LastUpdate = lastUpdateDateTime;
            Confirmed = confirmed;
            Deaths = deaths;
            Recovered = recovered;
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion Constructors
    }
}
