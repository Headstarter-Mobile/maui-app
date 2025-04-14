using Headstarter.Protos;
using Headstarter.ViewModels;
using System.Diagnostics;
using System.Windows.Input;

namespace Headstarter.Views;

public partial class CompanyPage : ContentPage
{
	public CompanyPage(int id = 1)
	{
		InitializeComponent();
        Task.Run(() => this.LoadViewModel(id)).Wait();
    }

    private async Task LoadViewModel(int id)
    {
        this.BindingContext = await Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<CompanyPageViewModel>().ForCompany(id);
    }
}