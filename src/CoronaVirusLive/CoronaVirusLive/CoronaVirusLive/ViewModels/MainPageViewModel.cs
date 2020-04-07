using CoronaVirusLive.Models;
using CoronaVirusLive.Services;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoronaVirusLive.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICaseService CaseService => DependencyService.Get<ICaseService>();
        private const int numEntries = 3; // number of history entries to grab;
        private readonly Dictionary<DateTime, IEnumerable<Case>> Cases = new Dictionary<DateTime, IEnumerable<Case>>();


        #region Constructors

        public MainPageViewModel()
        {
        }

        #endregion Constructors

        #region Messages
        #endregion Messages

        #region Properties

        string status = null;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        #endregion Properties

        #region Commands
        #endregion Commands

        #region Methods

        private async Task<Tuple<DateTime, IEnumerable<Case>>> GetCasesAsync(DateTime? queryDateTime = null)
        {
            IEnumerable<Case> cases = null;
            int days = 0;
            if (!queryDateTime.HasValue) queryDateTime = DateTime.Today;


            // continue querying until we get data.
            while (cases == null || cases.Count() == 0)
            {
                queryDateTime = queryDateTime.Value.AddDays(days--);
                cases = await CaseService.GetCasesByDate(queryDateTime.Value);
            }

            if (cases == null)
                return null;
            else
                return new Tuple<DateTime, IEnumerable<Case>>(queryDateTime.Value, cases);
        }

        public override async Task PrepareViewModelAsync()
        {
            IsBusy = true;

            bool isEnabled = await Analytics.IsEnabledAsync();
            if (!isEnabled) await Analytics.SetEnabledAsync(true);

            UpdateStatus("Attempting to get latest case statistics");

            DateTime? queryDateTime = DateTime.Today; // first query for today

            for (int i = 0; i < numEntries; i++)
            {
                // Query for the cases
                Tuple<DateTime, IEnumerable<Case>> casesAndDateTime = await GetCasesAsync(queryDateTime);
                if (casesAndDateTime == null) break;

                // Add to the dictionary
                Cases.Add(casesAndDateTime.Item1, casesAndDateTime.Item2);
                // query before this data's date
                queryDateTime = casesAndDateTime.Item1.AddDays(-1);
            }

            if (Cases.Count > 0)
            {
                // if we have cases, get the latest, and fire off the message bus.
                DateTime latestCase = Cases.Keys.OrderBy(x => x).FirstOrDefault();
                UpdateStatus($"Data found from {latestCase}");
                MessagingCenter.Send<MainPageViewModel, IEnumerable<Case>>(this, "CasesUpdated", Cases[latestCase]);
                MessagingCenter.Send<MainPageViewModel, Dictionary<DateTime, IEnumerable<Case>>>(this, "CasesUpdated", Cases);
            }
            else
            {
                // we don't have any data. Although this should never happen,
                // it's placed here in case the data source changes.... Which has happened a lot.
                UpdateStatus($"No data was found...");
            }

            IsBusy = false;
        }




        #endregion Methods

    }
}
