using CoronaVirusLive.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoronaVirusLive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;

        public ICommand NavigateCommand { get; set; }


        public MainPage()
        {
            InitializeComponent();


            BindingContext = this.viewModel = new MainPageViewModel();

            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            });

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.PrepareViewModelAsync();
        }
    }
}