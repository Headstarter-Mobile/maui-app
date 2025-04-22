using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Headstarter.Services;
using Plugin.LocalNotification;

namespace Headstarter.Droid;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize
)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        CreateNotificationChannel(); // Still required for Android 8+
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13+
        {
            const string notificationPermission = Android.Manifest.Permission.PostNotifications;

            if (CheckSelfPermission(notificationPermission) != Permission.Granted)
            {
                RequestPermissions(new[] { notificationPermission }, 1001);
            }
        }
    }

    private void CreateNotificationChannel()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(
                "default",
                "General Notifications",
                NotificationImportance.Default)
            {
                Description = "Used for general local notifications"
            };

            var manager = (NotificationManager)GetSystemService(NotificationService);
            manager.CreateNotificationChannel(channel);
        }
    }
}