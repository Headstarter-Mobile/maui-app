using Headstarter.Protos;
using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class RecruiterProfilePage : ContentPage
{
    public RecruiterProfilePage(User user = null)
    {
        InitializeComponent();
        Task.Run(() => this.LoadViewModel(user)).Wait();
    }

    private async Task LoadViewModel(User? user)
    {
        this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<RecruiterProfilePageViewModel>().LoadData(user);
    }
}