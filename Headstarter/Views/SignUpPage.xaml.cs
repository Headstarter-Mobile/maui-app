using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class SignupPage : ContentPage
{
    private bool _isPasswordVisible = false;

    public SignupPage()
    {
        InitializeComponent();
        BindingContext = Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<SignupPageViewModel>();
    }

    private async void NavigateToEmailVerificationCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmailVerificationPage((this.BindingContext as SignupPageViewModel).newUser, false));
    }

    private async void NavigateToLoginCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        PasswordEntry.IsPassword = !_isPasswordVisible;
        ConfirmPasswordEntry.IsPassword = !_isPasswordVisible;
    }

    private async void OnDayButtonClicked(object sender, EventArgs e)
    {
        var dayOptions = new string[31];
        for (int i = 0; i < 31; i++)
        {
            dayOptions[i] = (i + 1).ToString();
        }

        var daySelected = await DisplayActionSheet("Select Day", "Cancel", null, dayOptions);

        if (daySelected != null && daySelected != "Cancel")
        {
            DayButton.Text = daySelected;
        }
    }

    private async void OnMonthButtonClicked(object sender, EventArgs e)
    {
        var monthOptions = new string[]
        {
            "Януари", "Февруари", "Март", "Април", "Май", "Юни",
            "Юли", "Август", "Септември", "Октомври", "Ноември", "Декември"
        };

        var monthSelected = await DisplayActionSheet("Select Month", "Cancel", null, monthOptions);

        if (monthSelected != null && monthSelected != "Cancel")
        {
            MonthButton.Text = monthSelected;
        }
    }


    private async void OnYearButtonClicked(object sender, EventArgs e)
    {
        var yearOptions = new string[126];
        for (int i = 0; i < 126; i++)
        {
            yearOptions[i] = (2025 - i).ToString();
        }

        var yearSelected = await DisplayActionSheet("Select Year", "Cancel", null, yearOptions);

        if (yearSelected != null && yearSelected != "Cancel")
        {
            YearButton.Text = yearSelected;
        }
    }
}
