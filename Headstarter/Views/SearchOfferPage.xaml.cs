using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class SearchOfferPage : ContentPage
{
	public SearchOfferPage()
	{
		InitializeComponent();
        this.BindingContext = Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<SearchOfferPageViewModel>();
    }
}