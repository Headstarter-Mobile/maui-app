using System.Diagnostics;
using System.Windows.Input;

namespace Headstarter.Components;

public partial class NewsWidget : ContentView
{
	public string Title { get; set; }
	public static readonly BindableProperty TitleProperty = BindableProperty.Create(
		nameof(Title),
		typeof(string),
		typeof(NewsWidget));
	public string Description { get; set; }
	public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
		nameof(Description),
		typeof(string),
		typeof(NewsWidget));
	public string Image { get; set; }
	public static readonly BindableProperty ImagerProperty = BindableProperty.Create(
		nameof(Image),
		typeof(string),
		typeof(NewsWidget));
	public NewsWidget()
	{
		InitializeComponent();
	}
	public static readonly BindableProperty ViewNewsCommandProperty = BindableProperty.Create(
		nameof(ViewNewsCommand),
		typeof(ICommand),
		typeof(NewsWidget));

    public ICommand ViewNewsCommand
    {
        get => (ICommand)GetValue(ViewNewsCommandProperty);
        set => SetValue(ViewNewsCommandProperty, value);
    }

    private async void OnViewNews(int id)
    {
        try
        {
            await Shell.Current.GoToAsync($"newsDetails?id={id}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error navigating to news details: {ex.Message}");
        }
    }
}