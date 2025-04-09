namespace Headstarter.Views;
using Headstarter.ViewModels;

public partial class CompanyPage : ContentPage
{
    public CompanyPage()
    {
        InitializeComponent();
        this.BindingContext = new CompanyViewModel();
    }
}