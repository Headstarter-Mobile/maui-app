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
