namespace Headstarter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes
		Routing.RegisterRoute("RecruiterHomePage", typeof(Views.RecruiterHomePage));
		Routing.RegisterRoute("SpecialistHomePage", typeof(Views.SpecialistHomePage));
        Routing.RegisterRoute("LoginPage", typeof(Views.LoginPage));
        Routing.RegisterRoute("MainPage", typeof(Views.MainPage));
    }
}
