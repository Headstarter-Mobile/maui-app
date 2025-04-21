using Headstarter.Protos;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Headstarter.ViewModels;

public class EmailVerificationPageViewModel : INotifyPropertyChanged
{
    private string? _digit1;
    private string? _digit2;
    private string? _digit3;
    private string? _digit4;
    private string? _digit5;
    private string? _digit6;
    private User user;

    public User User { get => user; set { user = value; OnPropertyChanged(); } }

    public event PropertyChangedEventHandler? PropertyChanged;

    public EmailVerificationPageViewModel(User _user)
    {
        User = _user;
        NavigateToProfileOptionsCommand = new Command(OnNavigateToProfileOptions);
    }

    public string? Digit1
    {
        get => _digit1;
        set { _digit1 = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullCode)); }
    }

    public string? Digit2
    {
        get => _digit2;
        set { _digit2 = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullCode)); }
    }

    public string? Digit3
    {
        get => _digit3;
        set { _digit3 = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullCode)); }
    }

    public string? Digit4
    {
        get => _digit4;
        set { _digit4 = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullCode)); }
    }

    public string? Digit5
    {
        get => _digit5;
        set { _digit5 = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullCode)); }
    }

    public string? Digit6
    {
        get => _digit6;
        set { _digit6 = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullCode)); }
    }

    public string FullCode => $"{Digit1}{Digit2}{Digit3}{Digit4}{Digit5}{Digit6}";

    public ICommand NavigateToProfileOptionsCommand { get; }

    private void OnNavigateToProfileOptions()
    {
        // Logic to verify the code or navigate forward
        Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Code Entered", $"Code: {FullCode}", "OK");

        // Optionally navigate to next page using Shell or Navigation
        // await Shell.Current.GoToAsync("ProfileOptionsPage");
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
