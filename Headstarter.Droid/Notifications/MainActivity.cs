using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Headstarter.Interfaces;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;

namespace Headstarter.Notifications
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Fire-and-forget the permission request
            RequestNotificationPermissionAsync();

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
                string title = intent.GetStringExtra(NotificationManagerService.TitleKey);
                string message = intent.GetStringExtra(NotificationManagerService.MessageKey);

                var service = IPlatformApplication.Current.Services.GetService<INotificationManagerService>();
                service.ReceiveNotification(title, message);
            }
        }

        private async void RequestNotificationPermissionAsync()
        {
#if ANDROID
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13+
            {
                var status = await Permissions.RequestAsync<Permissions.PostNotifications>();
                if (status != PermissionStatus.Granted)
                {
                    // Handle the case where permission is denied
                    System.Diagnostics.Debug.WriteLine("Notification permission was not granted.");
                }
            }
#endif
        }
    }
}
