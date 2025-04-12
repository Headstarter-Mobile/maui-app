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
