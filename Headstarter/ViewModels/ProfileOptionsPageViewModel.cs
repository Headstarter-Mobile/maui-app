using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Headstarter.Protos;

namespace Headstarter.ViewModels;

public class ProfileOptionsPageViewModel : INotifyPropertyChanged
{
    private bool _workerSelected;
    private bool _recruiterSelected;
    private bool _workerRecruiterNotSelected = true;

    private string _linkedInUrl = "";
    private string _otherLink = "";
    private Company _company = new Company();

    public event PropertyChangedEventHandler? PropertyChanged;

    public ProfileOptionsPageViewModel()
    {
        SelectWorkerCommand = new Command(OnSelectWorker);
        SelectRecruiterCommand = new Command(OnSelectRecruiter);
        CompleteWorkerProfileCommand = new Command(OnWorkerProfileCompleted);
        CompleteRecruiterProfileCommand = new Command(OnRecruiterProfileCompleted);
        PickFileCommand = new Command(OnPickFile);
    }

    public bool WorkerSelected
    {
        get => _workerSelected;
        set { _workerSelected = value; OnPropertyChanged(); }
    }

    public bool RecruiterSelected
    {
        get => _recruiterSelected;
        set { _recruiterSelected = value; OnPropertyChanged(); }
    }

    public bool WorkerRecruiterNotSelected
    {
        get => _workerRecruiterNotSelected;
        set { _workerRecruiterNotSelected = value; OnPropertyChanged(); }
    }

    public string LinkedInUrl
    {
        get => _linkedInUrl;
        set { _linkedInUrl = value; OnPropertyChanged(); }
    }

    public string OtherLink
    {
        get => _otherLink;
        set { _otherLink = value; OnPropertyChanged(); }
    }

    public Company Company
    {
        get => _company;
        set { _company = value; OnPropertyChanged(); }
    }

    public string CompanyName
    {
        get => Company.Name;
        set { Company.Name = value; OnPropertyChanged(); OnPropertyChanged(nameof(Company)); }
    }

    public string CompanyWebsite
    {
        get => Company.Website;
        set { Company.Website = value; OnPropertyChanged(); OnPropertyChanged(nameof(Company)); }
    }

    public string CompanyDescription
    {
        get => Company.Description;
        set { Company.Description = value; OnPropertyChanged(); OnPropertyChanged(nameof(Company)); }
    }

    public ICommand SelectWorkerCommand { get; }
    public ICommand SelectRecruiterCommand { get; }
    public ICommand CompleteWorkerProfileCommand { get; }
    public ICommand CompleteRecruiterProfileCommand { get; }
    public ICommand PickFileCommand { get; }

    private void OnSelectWorker()
    {
        WorkerSelected = true;
        RecruiterSelected = false;
        WorkerRecruiterNotSelected = false;
    }

    private void OnSelectRecruiter()
    {
        RecruiterSelected = true;
        WorkerSelected = false;
        WorkerRecruiterNotSelected = false;
    }

    private async void OnWorkerProfileCompleted()
    {
        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Success", "Worker profile completed!", "OK");
    }

    private async void OnRecruiterProfileCompleted()
    {
        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Success", "Recruiter profile completed!", "OK");
    }

    private async void OnPickFile()
    {
        var result = await FilePicker.PickAsync();
        if (result != null)
        {
            string fileName = result.FileName;
            string filePath = result.FullPath;
            await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("File Selected", $"Name: {fileName}\nPath: {filePath}", "OK");
        }
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
