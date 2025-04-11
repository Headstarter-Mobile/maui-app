using Headstarter.Views;

namespace Headstarter;

public partial class App : Application
{
    public static IServiceProvider Services { get; set; }

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        
        Services = serviceProvider;

        // Run session check asynchronously
        _ = InitializeAppAsync();
    }

    private async Task InitializeAppAsync()
    {
        var sessionLoaded = await SessionService.Instance.LoadSessionAsync();

        // Set the actual MainPage based on session state
        MainPage = sessionLoaded
            ? new AppShell()
            : new NavigationPage(new LoginPage());
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
