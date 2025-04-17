using Headstarter.Protos;
using Headstarter.Services;
using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class JobOfferPage : ContentPage
{
	public JobOfferPage(int? positionId = null)
	{
		InitializeComponent();
        Task.Run(() => this.LoadViewModel(positionId)).Wait();
    }
    private async Task LoadViewModel(int? positionId)
    {
        if (positionId is null)
        {
            this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<JobOfferPageViewModel>().SetDummyData();
        }
        else
            this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<JobOfferPageViewModel>().ForPosition((int)positionId);
    }
}