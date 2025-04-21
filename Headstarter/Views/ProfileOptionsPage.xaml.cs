using Headstarter.ViewModels;
using System.ComponentModel;

namespace Headstarter.Views;

public partial class ProfileOptionsPage : ContentPage, INotifyPropertyChanged
{
    private bool _workerRecruiterNotSelected;
    public bool WorkerRecruiterNotSelected
    {
        get => _workerRecruiterNotSelected;
        set
        {
            _workerRecruiterNotSelected = value;
            OnPropertyChanged(nameof(WorkerRecruiterNotSelected));
        }
    }

    public ProfileOptionsPage(Protos.User newUser)
    {
        WorkerRecruiterNotSelected = true;
        InitializeComponent();
        BindingContext = Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<ProfileOptionsPageViewModel>().SetUser(newUser);
        WorkerSection.IsVisible = false;
        RecruiterSection.IsVisible = false;
    }

    private void OnWorkerSelected(object sender, EventArgs e)
    {
        WorkerRecruiterNotSelected = false;
        WorkerSection.IsVisible = true;
        RecruiterSection.IsVisible = false;

        WorkerButton.WidthRequest = 160;
        WorkerButton.HeightRequest = 75;
        WorkerButton.FontSize = 18;

        RecruiterButton.WidthRequest = 120;
        RecruiterButton.HeightRequest = 55;
        RecruiterButton.FontSize = 14;
    }

    private void OnRecruiterSelected(object sender, EventArgs e)
    {
        WorkerRecruiterNotSelected = true;
        WorkerSection.IsVisible = false;
        RecruiterSection.IsVisible = true;

        RecruiterButton.WidthRequest = 160;
        RecruiterButton.HeightRequest = 75;
        RecruiterButton.FontSize = 18;

        WorkerButton.WidthRequest = 120;
        WorkerButton.HeightRequest = 55;
        WorkerButton.FontSize = 14;
    }

    private async void OnWorkerRegister(object sender, EventArgs e)
    {
        await DisplayAlert("Success", "Worker profile completed!", "OK");
    }

    private async void OnRecruiterRegister(object sender, EventArgs e)
    {
        await DisplayAlert("Success", "Recruiter profile completed!", "OK");
    }

    private async void OnPickFileClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync();

        if (result != null)
        {
            string fileName = result.FileName;
            string filePath = result.FullPath;

            await DisplayAlert("File Selected", $"Name: {fileName}\nPath: {filePath}", "OK");
        }
    }

    //private async void OnPickProfilePicture(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        var result = await FilePicker.PickAsync(new PickOptions
    //        {
    //            FileTypes = FilePickerFileType.Images,
    //            PickerTitle = "Избери профилна снимка"
    //        });

    //        if (result != null)
    //        {
    //            using var stream = await result.OpenReadAsync();
    //            ProfilePicture.Source = ImageSource.FromStream(() => stream);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        await DisplayAlert("Грешка", $"Неуспешно качване на снимка: {ex.Message}", "OK");
    //    }
    //}
}
