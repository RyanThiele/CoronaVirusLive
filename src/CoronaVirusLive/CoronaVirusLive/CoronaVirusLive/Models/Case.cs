using System;
using System.Globalization;

namespace CoronaVirusLive.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Admin2 { get; set; }
        public string ProvinceState { get; set; }
        public string CountryRegion { get; set; }
        public DateTime LastUpdate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public string CombinedKey { get; set; }


        #region Constructors

        public Case()
        {

        }

        public Case(string line)
        {
            if (String.IsNullOrWhiteSpace(line)) return;
            string[] columns = line.Split(',');

            // variables
            string lastUpdatedDateTimeValue = null;
            DateTime lastUpdateDateTime = new DateTime();
            int id = 0, confirmed = 0, deaths = 0, recovered = 0, active = 0;
            double latitude = 0, longitude = 0;



            if (columns.Length > 0) int.TryParse(columns[0], out id);
            if (columns.Length > 1) Admin2 = columns[1];
            if (columns.Length > 2) ProvinceState = columns[2];
            if (columns.Length > 3) CountryRegion = columns[3];
            if (columns.Length > 4) lastUpdatedDateTimeValue = columns[4];
            if (columns.Length > 5) Double.TryParse(columns[5], out latitude);
            if (columns.Length > 6) Double.TryParse(columns[6], out longitude);
            if (columns.Length > 7) int.TryParse(columns[7], out confirmed);
            if (columns.Length > 8) int.TryParse(columns[8], out deaths);
            if (columns.Length > 9) int.TryParse(columns[9], out recovered);
            if (columns.Length > 10) int.TryParse(columns[10], out active);
            if (columns.Length > 11) CombinedKey = columns[11];



            DateTime.TryParse(lastUpdatedDateTimeValue, out lastUpdateDateTime);
            if (lastUpdateDateTime == new DateTime()) DateTime.TryParseExact(lastUpdatedDateTimeValue, "M/dd/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out lastUpdateDateTime);


            Id = id;
            LastUpdate = lastUpdateDateTime;
            Latitude = latitude;
            Longitude = longitude;
            Confirmed = confirmed;
            Deaths = deaths;
            Recovered = recovered;
            Active = active;
        }

        #endregion Constructors
    }
}
