using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Headstarter.ViewModels;

public class ForgottenPasswordPageViewModel : INotifyPropertyChanged
{
    private string _password;
    private string _confirmPassword;
    private bool _isPasswordVisible;

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand TogglePasswordVisibilityCommand { get; }
    public ICommand NavigateToProfileCommand { get; }

    public ForgottenPasswordPageViewModel()
    {
        TogglePasswordVisibilityCommand = new Command(TogglePasswordVisibility);
        NavigateToProfileCommand = new Command(OnNavigateToProfile);
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(); }
    }

    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set
        {
            if (_isPasswordVisible != value)
            {
                _isPasswordVisible = value;
                OnPropertyChanged();
            }
        }
    }

    private void TogglePasswordVisibility()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }

    private async void OnNavigateToProfile()
    {
        if (Password != ConfirmPassword)
        {
            await Application.Current.MainPage.DisplayAlert("Грешка", "Паролите не съвпадат", "OK");
            return;
        }

        if (!IsPasswordValid(Password))
        {
            await Application.Current.MainPage.DisplayAlert("Грешка", "Паролата не отговаря на изискванията", "OK");
            return;
        }

        await Application.Current.MainPage.DisplayAlert("Успех", "Паролата е зададена успешно", "OK");

        // await Shell.Current.GoToAsync("//ProfilePage");
    }

    private bool IsPasswordValid(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8) return false;
        if (!password.Any(char.IsUpper)) return false;
        if (!password.Any(char.IsLower)) return false;
        if (!password.Any(char.IsDigit)) return false;
        if (!password.Any(ch => "!@#$%^*".Contains(ch))) return false;
        return true;
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
