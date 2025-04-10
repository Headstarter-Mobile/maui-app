namespace Headstarter.Views;

public partial class ForgottenPasswordPage : ContentPage
{
    private bool _isPasswordVisible = false;

	public ForgottenPasswordPage()
	{
		InitializeComponent();
	}

    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        PasswordEntry.IsPassword = !_isPasswordVisible;
        ConfirmPasswordEntry.IsPassword = !_isPasswordVisible;
    }
    private async void NavigateToProfileCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WorkerProfilePage());
    }
}