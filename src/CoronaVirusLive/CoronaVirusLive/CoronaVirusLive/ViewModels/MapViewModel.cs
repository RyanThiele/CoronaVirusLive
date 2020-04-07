using CoronaVirusLive.CustomControls;
using CoronaVirusLive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CoronaVirusLive.ViewModels
{
    public class MapViewModel : BaseViewModel
    {

        private List<Pin> pins = new List<Pin>();


        #region Constructors

        public MapViewModel()
        {
            Title = "Map";

            MessagingCenter.Subscribe<BaseViewModel, string>(this, "Status", (sender, arg) =>
            {
                IsDataStatusVisible = arg != null;
                DataStatus = arg;
            });

            MessagingCenter.Subscribe<MapViewModel, string>(this, "CasesDataStatus", (sender, arg) =>
            {
                IsDataStatusVisible = arg != null;
                DataStatus = arg;
            });


            MessagingCenter.Subscribe<MainPageViewModel, Dictionary<DateTime, IEnumerable<Case>>>(this, "CasesUpdated", (sender, arg) =>
            {

                if (arg != null && arg.Count() != 0)
                {
                    // get the latest cases
                    DateTime? latestestDateTime = arg.Keys.OrderByDescending(x => x).FirstOrDefault();
                    DateTime? nextlatestestDateTime = arg.Keys.OrderByDescending(x => x).Skip(1).FirstOrDefault();

                    if (latestestDateTime == null)
                    {
                        InformNoUpdates();
                        return;
                    }

                    List<CustomPin> pins = new List<CustomPin>();

                    foreach (Case model in arg[latestestDateTime.Value])
                    {
                        CustomPin customPin = new CustomPin()
                        {
                            Position = new Position(model.Latitude, model.Longitude),
                            Label = $"Confirmed: {model.Confirmed} Dead: {model.Deaths} Recovered: {model.Recovered}",
                            Address = $"{model.Admin2}, {model.ProvinceState}, {model.CountryRegion}",
                            Confirmed = model.Confirmed,
                            Deaths = model.Deaths,
                            Recovered = model.Recovered,
                            CityRegion = model.Admin2,
                            StateProvince = model.ProvinceState,
                            Country = model.CountryRegion,
                            Type = PinType.Place
                        };

                        // add change
                        if (nextlatestestDateTime.HasValue)
                        {
                            Case nextLatestCase = arg[nextlatestestDateTime.Value]
                            .Where(m => m.Admin2 == customPin.CityRegion)
                            .Where(m => m.ProvinceState == customPin.StateProvince)
                            .Where(m => m.CountryRegion == customPin.Country)
                            .FirstOrDefault();

                            if (nextLatestCase != null)
                            {
                                customPin.ChangedAmountDateTime = nextlatestestDateTime.Value;
                                customPin.ConfirmedChangedAmount = customPin.Confirmed - nextLatestCase.Confirmed;
                                customPin.DeathsChangedAmount = customPin.Deaths - nextLatestCase.Deaths;
                                customPin.RecoveredChangedAmount = customPin.Recovered - nextLatestCase.Recovered;
                            }
                        }

                        pins.Add(customPin);
                    }

                    Status = $"Latest Update: {latestestDateTime.Value.ToString()}";

                    MessagingCenter.Send<MapViewModel, IEnumerable<CustomPin>>(this, "PinsUpdated", pins);
                }
                else
                {
                    InformNoUpdates();
                }
            });


        }


        private void InformNoUpdates()
        {
            Status = $"No updates found.";
            MessagingCenter.Send<MapViewModel, IEnumerable<Pin>>(this, "PinsUpdated", pins);
            return;
        }

        #endregion Constructors

        #region Messages




        #endregion Messages

        #region Properties

        //public ObservableCollection<Case> Cases { get; set; } = new ObservableCollection<Case>();

        string status = null;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }


        bool isDataStatusVisible = false;
        public bool IsDataStatusVisible
        {
            get { return isDataStatusVisible; }
            set { SetProperty(ref isDataStatusVisible, value); }
        }

        string dataStatus = null;
        public string DataStatus
        {
            get { return dataStatus; }
            set { SetProperty(ref dataStatus, value); }
        }

        public override Task PrepareViewModelAsync()
        {
            throw new NotImplementedException();
        }

        #endregion Properties


        #region Commands

        #endregion Commands

        #region Methods



        #endregion Methods





    }
}
