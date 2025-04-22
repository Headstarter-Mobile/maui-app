using Headstarter.Services;
using Headstarter.Views;
using Plugin.LocalNotification;

namespace Headstarter;

public partial class App : Application
{
    public int latestNotificationId = -1;
    private Services.INotificationService notificationService;
    public static IServiceProvider Services { get; set; }

    public App(IServiceProvider serviceProvider, Services.INotificationService _notificationService)
    {
        InitializeComponent();

        Services = serviceProvider;
        notificationService = _notificationService;

        // Run session check asynchronously
        _ = InitializeAppAsync();
        _ = StartPollingNotifications();
    }
    void ShowLocalNotification(string title, string body)
    {
        var notification = new NotificationRequest
        {
            NotificationId = new Random().Next(),
            Title = title,
            Description = body,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(1)
            }
        };

        LocalNotificationCenter.Current.Show(notification);
    }
    private async Task StartPollingNotifications()
    {
        while (true)
        {
            var notifications = await notificationService.GetUnseenMessagesAfter(latestNotificationId);
            foreach (var n in notifications)
            {
                ShowLocalNotification(n.Title, n.Text);
            }

            await Task.Delay(TimeSpan.FromMinutes(10));
        }
    }

    private async Task InitializeAppAsync()
    {
        var sessionLoaded = await SessionService.Instance.LoadSessionAsync();

        //if (sessionLoaded)
        //{
        //    await Shell.Current.GoToAsync("//HomePage");
        //}
        //else
        //{
        //    await Shell.Current.GoToAsync("//LoginPage");
        //}
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
