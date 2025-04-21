using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class NewsTemplatePage : ContentPage
{
    public NewsTemplatePage(string title, string description, string image, string content)
    {
        InitializeComponent();
        this.BindingContext = new NewsTemplateViewModel(content, image, description, title);
    }
}