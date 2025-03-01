using System.ComponentModel;
using System.Runtime.CompilerServices;
using Headstarter.Models;
using Headstarter.Services;

namespace Headstarter.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;

    public LoginViewModel(IUserService userService)
    {
        _userService = userService;
        LoginCommand = new Command(async () => await Login());
    }

    private string _username;
    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    private UserType _selectedUserType;
    public UserType SelectedUserType
    {
        get => _selectedUserType;
        set { _selectedUserType = value; OnPropertyChanged(); }
    }

    public Command LoginCommand { get; }

    private async Task Login()
    {
        User authenticatedUser = await _userService.AuthenticateUser(Username, Password, SelectedUserType);

        if (authenticatedUser != null)
        {
            // Navigate to the appropriate screen based on userType
            if (SelectedUserType == UserType.Specialist)
            {
                await Shell.Current.GoToAsync("//SpecialistHome");
            }
            else
            {
                await Shell.Current.GoToAsync("//RecruiterHome");
            }
        }
        else
        {
            // Display error message
            await App.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password.", "OK");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}