namespace Headstarter.Views;

public partial class AboutUsPage : ContentPage
{
	public AboutUsPage()
	{
		InitializeComponent();
	}
    //alex social

    private async void OnAlexPhoneTapped(object sender, EventArgs e)
    {
        var phoneNumber = "+359988329931";
        await Launcher.Default.OpenAsync($"tel:{phoneNumber}");
    }
    private async void OnAlexEmailTapped(object sender, EventArgs e)
    {
        var email = "alex_tsvetanov_2002@abv.bg";
        var subject = Uri.EscapeDataString("Inquiry from app");
        var body = Uri.EscapeDataString("");

        var uri = new Uri($"mailto:{email}?subject={subject}&body={body}");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnAlexFacebookTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.facebook.com/alex.tsvetanov.2002");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnAlexInstagramTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.instagram.com/coder_of_worlds/");
        await Launcher.Default.OpenAsync(uri);
    }

    //kiril social

    private async void OnKirilPhoneTapped(object sender, EventArgs e)
    {
        var phoneNumber = "+359988329931";
        await Launcher.Default.OpenAsync($"tel:{phoneNumber}");
    }
    private async void OnKirilEmailTapped(object sender, EventArgs e)
    {
        var email = "kiril.valkov@abv.bg";
        var subject = Uri.EscapeDataString("Inquiry from app");
        var body = Uri.EscapeDataString("");

        var uri = new Uri($"mailto:{email}?subject={subject}&body={body}");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnKirilFacebookTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.facebook.com/profile.php?id=100006587107124");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnKirilInstagramTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.instagram.com/kirilvalkov_/");
        await Launcher.Default.OpenAsync(uri);
    }

    //boris social

    private async void OnBorisPhoneTapped(object sender, EventArgs e)
    {
        var phoneNumber = "+359888440107";
        await Launcher.Default.OpenAsync($"tel:{phoneNumber}");
    }
    private async void OnBorisEmailTapped(object sender, EventArgs e)
    {
        var email = "bobovlahov@gmail.com";
        var subject = Uri.EscapeDataString("Inquiry from app");
        var body = Uri.EscapeDataString("");

        var uri = new Uri($"mailto:{email}?subject={subject}&body={body}");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnBorisFacebookTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.facebook.com/profile.php?id=100010619388853");
        await Launcher.Default.OpenAsync(uri);
    }
    private async void OnBorisInstagramTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.instagram.com/boris_vlahov/");
        await Launcher.Default.OpenAsync(uri);
    }
}