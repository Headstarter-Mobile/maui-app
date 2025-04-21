using Headstarter.Protos;
using Headstarter.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace Headstarter.Components;

public partial class JobOfferWidget : ContentView
{
    public static readonly BindableProperty PositionProperty = BindableProperty.Create(
        nameof(Position),
        typeof(Position),
        typeof(JobOfferWidget),
        propertyChanged: OnPositionChanged);

    public Position Position
    {
        get => (Position)GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    public JobOfferWidget()
    {
        InitializeComponent();
        if (ViewPositionCommand == null)
        {
            ViewPositionCommand = new Command<Position>(OnViewPosition);
        }
    }

    private static void OnPositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is JobOfferWidget widget && newValue is Position newPosition)
        {
            widget.BindingContext = newPosition;
        }
    }

    public static readonly BindableProperty ViewPositionCommandProperty = BindableProperty.Create(
    nameof(ViewPositionCommand),
    typeof(ICommand),
    typeof(JobOfferWidget));

    public ICommand ViewPositionCommand
    {
        get => (ICommand)GetValue(ViewPositionCommandProperty);
        set => SetValue(ViewPositionCommandProperty, value);
    }

    private async void OnViewPosition(Position position)
    {
        try
        {
            await Navigation.PushAsync(new JobOfferPage(Position.Id));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error navigating to position details: {ex.Message}");
        }
    }
    private async void NavigateToJobsCommand(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new JobOfferPage(Position.Id));
    }
}