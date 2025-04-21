using Headstarter.Protos;
using Headstarter.Views;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Headstarter.ViewModels;
public class SignupPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // Properties
    private string firstName;
    public string FirstName
    {
        get => firstName;
        set { firstName = value; OnPropertyChanged(); }
    }

    private string lastName;
    public string LastName
    {
        get => lastName;
        set { lastName = value; OnPropertyChanged(); }
    }

    private string phone;
    public string Phone
    {
        get => phone;
        set { phone = value; OnPropertyChanged(); }
    }

    private string email;
    public string Email
    {
        get => email;
        set { email = value; OnPropertyChanged(); }
    }

    private string password;
    public string Password
    {
        get => password;
        set { password = value; OnPropertyChanged(); }
    }

    private string confirmPassword;
    public string ConfirmPassword
    {
        get => confirmPassword;
        set { confirmPassword = value; OnPropertyChanged(); }
    }

    private bool acceptTerms;
    public bool AcceptTerms
    {
        get => acceptTerms;
        set { acceptTerms = value; OnPropertyChanged(); }
    }

    private bool isPasswordVisible;
    public bool IsPasswordVisible
    {
        get => isPasswordVisible;
        set { isPasswordVisible = value; OnPropertyChanged(); }
    }

    private string birthDay = "Ден";
    public string BirthDay
    {
        get => birthDay;
        set { birthDay = value; OnPropertyChanged(); }
    }

    private string birthMonth = "Месец";
    public string BirthMonth
    {
        get => birthMonth;
        set { birthMonth = value; OnPropertyChanged(); }
    }

    private string birthYear = "Година";
    public string BirthYear
    {
        get => birthYear;
        set { birthYear = value; OnPropertyChanged(); }
    }

    public User newUser
    {
        get => new User
        {
            CreatedAt = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
            Email = email,
            Name = FirstName + " " + LastName,
            Password = Password,
            UpdatedAt = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
        };
    }

    // Commands
    public ICommand TogglePasswordVisibilityCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand NavigateToLoginCommand { get; }

    public SignupPageViewModel()
    {
        TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
        RegisterCommand = new Command(OnRegister);
        NavigateToLoginCommand = new Command(OnNavigateToLogin);
    }

    private void TogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    private async void OnRegister()
    {
        if (!IsValid()) return;

        User newUser = new User
        {
            CreatedAt = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
            Email = Email,
            Name = FirstName + " " + LastName,
            Password = Password,
            UpdatedAt = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture),
        };

        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Успех", "Регистрацията е успешна!", "OK");

        ProfileOptionsPage options = new ProfileOptionsPage(newUser);
        await Shell.Current.Navigation.PushAsync(options);
        // Navigate to email verification or home
    }

    private async void OnNavigateToLogin()
    {
        await Shell.Current.GoToAsync("login");
    }

    private bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            return ShowError("Моля, въведете валидни имена.");

        if (!Regex.IsMatch(Phone ?? "", @"^\d{10}$"))
            return ShowError("Въведете валиден телефонен номер (10 цифри).");

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            return ShowError("Невалиден имейл адрес.");

        if (Password != ConfirmPassword)
            return ShowError("Паролите не съвпадат.");

        if (!ValidatePassword(Password))
            return ShowError("Паролата не отговаря на изискванията.");

        if (!AcceptTerms)
            return ShowError("Моля, приемете общите условия.");

        return true;
    }

    private bool ShowError(string message)
    {
        Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Грешка", message, "OK");
        return false;
    }

    private bool ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return false;

        return password.Length >= 8 &&
                Regex.IsMatch(password, @"[A-Z]") &&
                Regex.IsMatch(password, @"[a-z]") &&
                Regex.IsMatch(password, @"[0-9]") &&
                Regex.IsMatch(password, @"[\W_]");
    }

    private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
