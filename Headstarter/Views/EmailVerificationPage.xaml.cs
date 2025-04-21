using Headstarter.Protos;
using Headstarter.ViewModels;
using Microsoft.Maui.ApplicationModel.Communication;

namespace Headstarter.Views;

public partial class EmailVerificationPage : ContentPage
{
    private bool toSignIn;
    private User user;
    public EmailVerificationPage(User _user, bool _toSignIn)
    {
        toSignIn = _toSignIn;
        user = _user;
        InitializeComponent();
        BindingContext = new EmailVerificationPageViewModel(user);
        Digit1.TextChanged += OnDigitEntered;
        Digit2.TextChanged += OnDigitEntered;
        Digit3.TextChanged += OnDigitEntered;
        Digit4.TextChanged += OnDigitEntered;
        Digit5.TextChanged += OnDigitEntered;
        Digit6.TextChanged += OnDigitEntered;
        ContinueButton.Clicked += NavigateToProfileOptionsCommand;
    }

    private void OnDigitEntered(object? sender, TextChangedEventArgs e)
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

    private async void NavigateToProfileOptionsCommand(object? sender, EventArgs e)
    {
        if (!toSignIn)
            await Navigation.PushAsync(new ProfileOptionsPage(user));
        else
            await Shell.Current.GoToAsync("login");
    }
}