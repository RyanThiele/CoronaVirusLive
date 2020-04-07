using CoronaVirusLive.CustomControls;
using Windows.UI.Xaml.Controls;

namespace CoronaVirusLive.UWP
{
    public sealed partial class XamarinMapOverlay : UserControl
    {
        CustomPin customPin;

        public XamarinMapOverlay(CustomPin pin)
        {
            this.InitializeComponent();
            customPin = pin;
            SetupData();
        }

        void SetupData()
        {
            ConfirmedAmountControl.Amount = customPin.Confirmed;
            DeathAmountControl.Amount = customPin.Deaths;
            RecoveredAmountControl.Amount = customPin.Recovered;

            if (customPin.ConfirmedChangedAmount == 0)
                ConfirmedAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.None;
            else if (customPin.ConfirmedChangedAmount > 0)
                ConfirmedAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.Up;
            else
                ConfirmedAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.Down;

            if (customPin.Deaths == 0)
                DeathAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.None;
            else if (customPin.Deaths > 0)
                DeathAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.Up;
            else
                DeathAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.Down;

            if (customPin.Recovered == 0)
                RecoveredAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.None;
            else if (customPin.Recovered > 0)
                RecoveredAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.Up;
            else
                RecoveredAmountControl.ChangeType = Uwp.AmountControl.ChangeTypes.Down;

            Address.Text = customPin.Address;
        }

    }
}
