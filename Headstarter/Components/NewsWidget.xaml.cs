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
            // Assuming navigation logic is related to the title, you can adjust if ID is needed
            await Shell.Current.GoToAsync($"newsDetails?title={Title}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }
}
