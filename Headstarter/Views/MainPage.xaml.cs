using Headstarter.ViewModels;

namespace Headstarter.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<MainPageViewModel>();
        }

        private async void OnAboutUsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutUsPage());
        }

        private async void NavigateToSearchOfferCommand(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchOfferPage());
        }
        private async void NavigateToJobOfferCommand(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JobOfferPage());
        }
    }
}
