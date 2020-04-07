using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CoronaVirusLive.Uwp
{
    public sealed partial class AmountControl : UserControl
    {
        public AmountControl()
        {
            this.InitializeComponent();
        }



        public int Amount
        {
            get { return (int)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(int), typeof(AmountControl), new PropertyMetadata(0, OnAmountChanged));

        private static void OnAmountChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AmountControl owner = o as AmountControl;
            if (owner == null) return;

            int? amount = e.NewValue as int?;
            if (amount.HasValue)
                owner.AmountTextBox.Text = amount.ToString();
            else
                owner.AmountTextBox.Text = "0";
        }


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AmountControl), new PropertyMetadata(0, OnTitleChanged));

        private static void OnTitleChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AmountControl owner = o as AmountControl;
            if (owner == null) return;

            owner.TitleTextBox.Text = e.NewValue as string;
        }


        public enum ChangeTypes
        {
            None,
            Up,
            Down
        }






        public ChangeTypes ChangeType
        {
            get { return (ChangeTypes)GetValue(ChangeTypeProperty); }
            set { SetValue(ChangeTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeTypeProperty =
            DependencyProperty.Register("ChangeType", typeof(ChangeTypes), typeof(AmountControl), new PropertyMetadata(0, OnChangeTypeChanged));




        private static void OnChangeTypeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            AmountControl owner = o as AmountControl;
            if (owner == null) return;

            ChangeTypes? changeType = e.NewValue as ChangeTypes?;

            if (!changeType.HasValue)
            {
                owner.ChangedPath.Visibility = Visibility.Collapsed;
                return;
            }

            if (changeType == ChangeTypes.Up)
            {
                owner.ChangedPath.Visibility = Visibility.Visible;
                owner.ChangePathOffsetColor.Color = Colors.Red;
                owner.ChangedPathRenderTransorm.Angle = 0;
            }
            else if (changeType == ChangeTypes.Down)
            {
                owner.ChangedPath.Visibility = Visibility.Visible;
                owner.ChangePathOffsetColor.Color = Colors.Green;
                owner.ChangedPathRenderTransorm.Angle = 180;
            }
            else
            {
                owner.ChangedPath.Visibility = Visibility.Collapsed;
            }

        }


    }
}
