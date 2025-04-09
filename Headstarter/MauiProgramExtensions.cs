﻿using Headstarter.Interfaces;
using Headstarter.Services;
using Headstarter.ViewModels;
using Headstarter.Views;
using Microsoft.Extensions.Logging;

namespace Headstarter;

public static class MauiProgramExtensions
{
	public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
	{

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
#if ANDROID
            builder.Services.AddTransient<INotificationManagerService, Headstarter.Services.Android.NotificationManagerService>();
#elif IOS
            builder.Services.AddTransient<INotificationManagerService, Headstarter.Services.iOS.NotificationManagerService>();
#elif MACCATALYST
            builder.Services.AddTransient<INotificationManagerService, Headstarter.Services.MacCatalyst.NotificationManagerService>();
#elif WINDOWS
            builder.Services.AddTransient<INotificationManagerService, Headstarter.Services.Windows.NotificationManagerService>();          
#endif
#if DEBUG
        builder.Logging.AddDebug();
#endif
		// Register services
		builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<IUserService, UserService>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<LoginPage>();

		return builder;
	}
}
