using Headstarter.Protos;
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
    }

    private static void OnPositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is JobOfferWidget widget && newValue is Position newPosition)
        {
            widget.BindingContext = newPosition;
        }
    }

    public static readonly BindableProperty ViewPositionCommandProperty = BindableProperty.Create(nameof(ViewPositionCommand), typeof(ICommand), typeof(JobOfferWidget));

    public ICommand ViewPositionCommand
    {
        get => (ICommand)GetValue(ViewPositionCommandProperty);
        set => SetValue(ViewPositionCommandProperty, value);
    }
}
