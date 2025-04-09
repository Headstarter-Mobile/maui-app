using System.ComponentModel;
using System.Runtime.CompilerServices;
using Headstarter.Services;
using Headstarter.Protos;

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

    private UserRole _selectedUserType;
    public UserRole SelectedUserType
    {
        get => _selectedUserType;
        set { _selectedUserType = value; OnPropertyChanged(); }
    }

    public Command LoginCommand { get; }

    private async Task Login()
    {
        User authenticatedUser = _userService.AuthenticateUser(Username, Password);

        if (authenticatedUser != null)
        {
            // Navigate to the appropriate screen based on userType
            if (SelectedUserType == UserRole.Candidate)
            {
                await Shell.Current.GoToAsync("SpecialistHomePage");
            }
            else
            {
                await Shell.Current.GoToAsync("RecruiterHomePage");
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