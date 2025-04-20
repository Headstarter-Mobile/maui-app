using Headstarter.Components;
using Headstarter.Services;
using Headstarter.ViewModels;
using Headstarter.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

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
                fonts.AddFont("Boldnose-Regular.ttf", "Boldnose");
                fonts.AddFont("Barriecito-Regular.ttf", "Barriecito");
                fonts.AddFont("LilitaOne-Regular.ttf", "LilitaOne");
                fonts.AddFont("materialdesignicons-webfont.ttf", "icons");
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
        builder.Services.AddSingleton<GrpcService>();
        builder.Services.AddTransient<IApplicationService, ApplicationService>();
        // builder.Services.AddTransient<IAuthTokenService, AuthTokenService>();
        builder.Services.AddTransient<ICompanyService, CompanyService>();
        builder.Services.AddTransient<INotificationService, NotificationService>();
        builder.Services.AddTransient<IOfficeService, OfficeService>();
        builder.Services.AddTransient<IPasswordService, PasswordService>();
        builder.Services.AddTransient<IPositionService, PositionService>();
        builder.Services.AddTransient<IUserService, UserService>();

        // ViewModels
        builder.Services.AddSingleton<CompanyPageViewModel>();
        builder.Services.AddSingleton<EmailVerificationPageViewModel>();
        builder.Services.AddSingleton<ForgottenPasswordPageViewModel>();
        builder.Services.AddSingleton<JobOfferPageViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<ProfileOptionsPageViewModel>();
        builder.Services.AddSingleton<ProfilePageViewModel>();
        builder.Services.AddSingleton<RecruiterProfilePageViewModel>();
        builder.Services.AddSingleton<SignupPageViewModel>();
        builder.Services.AddSingleton<WorkerProfilePageViewModel>();
        builder.Services.AddSingleton<SearchOfferPageViewModel>();
        builder.Services.AddSingleton<NewsTemplateViewModel>();

        // Views
        builder.Services.AddSingleton<CompanyPage>();
        builder.Services.AddSingleton<EmailVerificationPage>();
        builder.Services.AddSingleton<ForgottenPasswordPage>();
        builder.Services.AddSingleton<JobOfferWidget>();
        builder.Services.AddSingleton<NewsWidget>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ProfileOptionsPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<RecruiterProfilePage>();
        builder.Services.AddSingleton<SignupPage>();
        builder.Services.AddSingleton<WorkerProfilePage>();
        builder.Services.AddSingleton<SearchOfferPage>();
        builder.Services.AddSingleton<NewsPage>();
        builder.Services.AddSingleton<NewsTemplatePage>();
        builder.Services.AddSingleton<NotRecruiterPage>();
        builder.Services.AddSingleton<NotWorkerPage>();
        builder.Services.AddSingleton<QuestionsPage>();

        return builder;
    }
}
