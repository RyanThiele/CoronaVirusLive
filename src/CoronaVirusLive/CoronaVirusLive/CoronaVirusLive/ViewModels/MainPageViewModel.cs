using CoronaVirusLive.Models;
using CoronaVirusLive.Services;
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


        #region Constructors
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

        public async Task GetCasesAsync()
        {
            Status = "Attempting to get latest case statistics";

            IEnumerable<Case> cases = null;
            int days = 0;
            DateTime queryDateTime = DateTime.Today;

            // continue querying until we get data.
            while (cases == null || cases.Count() == 0)
            {
                queryDateTime = DateTime.Today.AddDays(days--);
                cases = await CaseService.GetCasesByDate(queryDateTime);
            }

            Status = $"Data found from {queryDateTime.ToString()}";

            MessagingCenter.Send<MainPageViewModel, IEnumerable<Case>>(this, "CasesUpdated", cases);
        }

        #endregion Methods

    }
}
