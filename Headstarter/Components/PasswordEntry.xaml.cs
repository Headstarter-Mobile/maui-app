using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Headstarter.Components;

public partial class PasswordEntryView : ContentView
{
    public PasswordEntryView()
    {
        InitializeComponent();
    }

    private void OnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        ToggleIcon.Text = PasswordEntry.IsPassword
            ? (string)Application.Current.Resources["IconEyeOff"]
            : (string)Application.Current.Resources["IconEye"];
    }

    public ICommand ToggleVisibilityCommand { get; }

    // Bindable Properties

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(PasswordEntryView), default(string), BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(PasswordEntryView), default(string));

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty PlaceholderColorProperty =
        BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(PasswordEntryView), Colors.Gray);

    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PasswordEntryView), Colors.Black);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(PasswordEntryView), 14.0);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty EntryBackgroundColorProperty =
        BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(PasswordEntryView), Colors.Transparent);

    public Color EntryBackgroundColor
    {
        get => (Color)GetValue(EntryBackgroundColorProperty);
        set => SetValue(EntryBackgroundColorProperty, value);
    }
}
