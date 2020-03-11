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

            using (WebClient client = new WebClient())
            {
                //client.BaseAddress = baseUri;

                for (int i = 0; i < days; i++)
                {
                    byte[] data = null;

                    try
                    {

                        string fileName = $"{baseUri}/{earlestDate.AddDays(i).ToStandardDateString()}.csv";
                        data = await client.DownloadDataTaskAsync(new Uri(fileName));
                        if (data == null) continue;

                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            using (StreamReader reader = new StreamReader(ms))
                            {
                                int index = 0;

                                while (reader.Peek() != -1)
                                {
                                    string line = await reader.ReadLineAsync();
                                    if (index++ == 0) continue;
                                    cases.Add(new Case(index, line));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    if (data == null) continue;

                }
            }

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Case>> GetCasesByDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}