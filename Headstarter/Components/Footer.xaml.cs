using Headstarter.Views;

namespace Headstarter.Components;

public partial class Footer : ContentView
{
	public Footer()
	{
		InitializeComponent();
	}
    private async void OnAboutUsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AboutUsPage());
    }

    private async void OnQuestionsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QuestionsPage());
    }

    //headstarter social

    private async void OnHSPhoneTapped(object sender, EventArgs e)
    {
        var phoneNumber = "+359988329931"; 
        await Launcher.Default.OpenAsync($"tel:{phoneNumber}");
    }
    private async void OnHSEmailTapped(object sender, EventArgs e)
    {
        var email = "contact_us@headstarter.eu";
        var subject = Uri.EscapeDataString("Inquiry from app"); 
        var body = Uri.EscapeDataString(""); 

        var uri = new Uri($"mailto:{email}?subject={subject}&body={body}");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnHSFacebookTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.facebook.com/headstarterbg");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnHSInstagramTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.instagram.com/headstarter_company/");
        await Launcher.Default.OpenAsync(uri);
    }
}