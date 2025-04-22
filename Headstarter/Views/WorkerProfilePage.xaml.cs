using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class WorkerProfilePage : ContentPage
{
    public WorkerProfilePage(int? id)
    {
        InitializeComponent();
        Task.Run(() => this.LoadViewModel(id)).Wait();
    }

    private async Task LoadViewModel(int? id)
    {
        this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<WorkerProfilePageViewModel>().LoadData(id);
    }
}