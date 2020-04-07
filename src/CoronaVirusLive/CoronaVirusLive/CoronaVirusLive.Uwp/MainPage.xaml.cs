// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CoronaVirusLive.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("MvudEfhbXHE81AJuyWiv~O6VNZq9DKn-j3HoDg4lklg~AhrpWtXyPOEhsHYobXppLFHL2UxCOmS6DFDipj6I7euUT9FbR1HSHu2CeqsjO7p5");
            LoadApplication(new CoronaVirusLive.App());
        }
    }
}
