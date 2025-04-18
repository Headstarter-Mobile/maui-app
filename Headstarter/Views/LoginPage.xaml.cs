using Headstarter.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Headstarter.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        this.BindingContext = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<LoginPageViewModel>();
    }

    private async void LoginValidation(object sender, EventArgs e)
    {
        await (this.BindingContext as LoginPageViewModel).LoginValidation();
    }

    private async void NavigateToProfileCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkerProfilePage());
    }

    private async void NavigateToNewPassVerifCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmailVerificationPage((this.BindingContext as LoginPageViewModel).Email));
    }

    private async void NavigateToSignupCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }

}
