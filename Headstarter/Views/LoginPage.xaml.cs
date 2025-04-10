namespace Headstarter.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void NavigateToProfileCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkerProfilePage());
    }

    private async void NavigateToNewPassVerifCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPassVerificationPage());
    }

    private async void NavigateToSignupCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignupPage());
    }

}
