using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class RecruiterProfilePage : ContentPage
{
	public RecruiterProfilePage(int? id = null)
	{
		InitializeComponent();
        Task.Run(() => this.LoadViewModel(id)).Wait();
    }

    private async Task LoadViewModel(int? id)
    {
        this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<RecruiterProfilePageViewModel>().LoadData(id);
    }
}