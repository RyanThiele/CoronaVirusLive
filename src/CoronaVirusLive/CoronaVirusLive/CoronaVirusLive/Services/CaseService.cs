using CoronaVirusLive.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CoronaVirusLive.Services
{
    public class CaseService : ICaseService
    {
        private readonly string baseUri = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports";
        private readonly DateTime earlestDate = new DateTime(2020, 01, 22);

        #region Helpers

        #endregion


        public async Task<IEnumerable<Case>> GetCasesAsync()
        {
            List<Case> cases = new List<Case>();
            int days = (DateTime.Today - earlestDate).Days;

            for (int i = 0; i < days; i++)
            {
                IEnumerable<Case> models = await GetCasesByDate(earlestDate.AddDays(i));
                if (models != null) cases.AddRange(models);
            }

            return cases;
        }

        public async Task<IEnumerable<Case>> GetCasesByDate(DateTime date)
        {
            List<Case> models = new List<Case>();

            using (WebClient client = new WebClient())
            {
                byte[] data = null;

                try
                {

                    string fileName = $"{baseUri}/{date.ToStandardDateString()}.csv";
                    data = await client.DownloadDataTaskAsync(new Uri(fileName));
                    if (data == null) return null;

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        using (StreamReader reader = new StreamReader(ms))
                        {
                            int index = 0;

                            while (reader.Peek() != -1)
                            {
                                string line = await reader.ReadLineAsync();
                                if (index++ == 0) continue;

                                Case model = new Case((date.DayOfYear * 100) + index, line);
                                if (model == null) continue;

                                models.Add(model);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return models;

            }
        }
    }
}