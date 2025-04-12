using Android.App;
using Android.Content.PM;
using Android.OS;
using Headstarter.Interfaces;
using Headstarter.Notifications;
using Android.Content;

namespace Headstarter.Droid;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop)]
public class MainActivity : MauiAppCompatActivity
{
    PermissionStatus status =  Permissions.RequestAsync<NotificationPermission>().Result;
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        CreateNotificationFromIntent(Intent);
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);

        CreateNotificationFromIntent(intent);
    }

    static void CreateNotificationFromIntent(Intent intent)
    {
        if (intent?.Extras != null)
        {
            string title = intent.GetStringExtra(Headstarter.Services.NotificationManagerService.TitleKey);
            string message = intent.GetStringExtra(Headstarter.Services.NotificationManagerService.MessageKey);

            var service = IPlatformApplication.Current.Services.GetService<INotificationManagerService>();
            service.ReceiveNotification(title, message);
        }
    }
}