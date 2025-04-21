using Headstarter.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace Headstarter.Components;

public partial class NewsWidget : ContentView
{
    public NewsWidget()
    {
        InitializeComponent();
        ViewNewsCommand = new Command(OnViewNews);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(NewsWidget),
        string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description),
        typeof(string),
        typeof(NewsWidget),
        string.Empty);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly BindableProperty ImageProperty = BindableProperty.Create(
        nameof(Image),
        typeof(string),
        typeof(NewsWidget),
        string.Empty);

    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public static readonly BindableProperty NewsContentProperty = BindableProperty.Create(
        nameof(NewsContent),
        typeof(string),
        typeof(NewsWidget),
        string.Empty);

    public string NewsContent
    {
        get => (string)GetValue(NewsContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public static readonly BindableProperty ViewNewsCommandProperty = BindableProperty.Create(
        nameof(ViewNewsCommand),
        typeof(ICommand),
        typeof(NewsWidget),
        null);

    public ICommand ViewNewsCommand
    {
        get => (ICommand)GetValue(ViewNewsCommandProperty);
        set => SetValue(ViewNewsCommandProperty, value);
    }

    private async void OnViewNews()
    {
        try
        {
            await Navigation.PushAsync(new NewsTemplatePage(Title, Description, Image, NewsContent));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }
    private async void NavigateToNewsTempCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewsTemplatePage(Title, Description, Image, NewsContent));
    }
}
