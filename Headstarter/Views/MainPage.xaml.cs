using Headstarter.Services;
using Headstarter.ViewModels;

namespace Headstarter.Views;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void OnLoginClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new LoginPage(new LoginViewModel(new UserService(new HttpClient())))); // Navigate to sign up page.
	}
}

