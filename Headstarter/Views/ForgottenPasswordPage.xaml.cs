using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class ForgottenPasswordPage : ContentPage
{
    public ForgottenPasswordPage()
    {
        InitializeComponent();
        BindingContext = new ForgottenPasswordPageViewModel();
    }
}
