using Headstarter.Protos;
using Headstarter.Services;
using Headstarter.Views;
using Plugin.LocalNotification;
using System.Diagnostics.Contracts;

namespace Headstarter.ViewModels;

public class LoginPageViewModel
{
    private readonly IUserService _userService;

    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public User User { get => new User() { Email = Email }; }
    public LoginPageViewModel(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));

        RememberMe = false;
        Email = string.Empty;
        Password = string.Empty;
    }

    internal async Task LoginValidation()
    {
        LoggedUserData? loggedUser = await _userService.LoginUser(new()
        {
            Email = Email,
            Password = Password
        });
        if (loggedUser != null)
        {
            await SessionService.Instance.SaveSessionAsync(loggedUser.UserData, loggedUser.Token);
            await LocalNotificationCenter.Current.Show(new()
            {
                NotificationId = new Random().Next(),
                Title = "Headstarter login",
                Description = "User " + loggedUser.UserData.Name + " has logged on.",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                },
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                {
                    ChannelId = "default"
                },
                Windows = new Plugin.LocalNotification.WindowsOption.WindowsOptions
                {
                    LaunchAppWhenTapped = false
                }
            });
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