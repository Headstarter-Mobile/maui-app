using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class CreateOfferPage : ContentPage
{
    public CreateOfferPage()
    {
        InitializeComponent();
        Task.Run(() => this.LoadViewModel()).Wait();
    }

    private async Task LoadViewModel()
    {
        this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<CreateOfferPageViewModel>().LoadCompany();
    }
}