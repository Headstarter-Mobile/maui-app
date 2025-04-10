namespace Headstarter.Views;

public partial class EmailVerificationPage : ContentPage
{
	public EmailVerificationPage()
	{
		InitializeComponent();
    }

    private void OnDigitEntered(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry currentEntry)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue)) 
            {
                if (currentEntry == Digit1) Digit2.Focus();
                else if (currentEntry == Digit2) Digit3.Focus();
                else if (currentEntry == Digit3) Digit4.Focus();
                else if (currentEntry == Digit4) Digit5.Focus();
                else if (currentEntry == Digit5) Digit6.Focus();
            }
            else 
            {
                if (currentEntry == Digit6) Digit5.Focus();
                else if (currentEntry == Digit5) Digit4.Focus();
                else if (currentEntry == Digit4) Digit3.Focus();
                else if (currentEntry == Digit3) Digit2.Focus();
                else if (currentEntry == Digit2) Digit1.Focus();
            }
        }
    }

    private async void NavigateToProfileOptionsCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfileOptionsPage());
    }

}