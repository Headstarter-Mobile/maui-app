using Headstarter.Protos;
using Headstarter.Services;
using Headstarter.Views;

namespace Headstarter.ViewModels;

public class LoginPageViewModel
{
    private readonly IUserService _userService;

    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public LoginPageViewModel(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        RememberMe = false;
        Email = string.Empty;
        Password = string.Empty;
    }

    internal async Task LoginValidation()
    {
        LoggedUserData? loggedUser = await _userService.AuthenticateUser(Email, Password);
        if (loggedUser != null)
        {
            await SessionService.Instance.SaveSessionAsync(loggedUser.UserData, loggedUser.Token);

            if (loggedUser.UserData.Type == UserRole.Recruiter)
                await Shell.Current.GoToAsync(nameof(RecruiterProfilePage));
            else if (loggedUser.UserData.Type == UserRole.Candidate)
                await Shell.Current.GoToAsync(nameof(WorkerProfilePage));
        }
        else
        {
            await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }
}