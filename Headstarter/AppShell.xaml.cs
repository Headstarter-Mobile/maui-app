using System.Windows.Input;

namespace Headstarter
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(Views.MainPage));
            Routing.RegisterRoute("login", typeof(Views.LoginPage));
            Routing.RegisterRoute("signup", typeof(Views.SignupPage));
            Routing.RegisterRoute("profile", typeof(Views.ProfilePage));


            NavigateToLoginCommand = new Command(async () => await Shell.Current.GoToAsync("login"));

            NavigateToSignUpCommand = new Command(async () => await Shell.Current.GoToAsync("signup"));

            NavigateToProfileCommand = new Command(async () => await Shell.Current.GoToAsync("profile"));

            Navigating += (s, e) => Shell.Current.FlyoutIsPresented = false;

            BindingContext = this;
        }

        public ICommand NavigateToLoginCommand { get; private set; }
        public ICommand NavigateToSignUpCommand { get; private set; }
        public ICommand NavigateToProfileCommand { get; private set; }

    }
}
