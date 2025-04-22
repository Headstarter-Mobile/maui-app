using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Headstarter.Protos;
using Headstarter.Services;

namespace Headstarter.ViewModels;

public class WorkerProfilePageViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private readonly IPositionService _positionService;
    private readonly IApplicationService _applicationService;

    private User _user = new()
    {
        Name = "Boris Vlahov",
        Email = "bobovlahov@gmail.com",
        Type = UserRole.Candidate
    };

    public WorkerProfilePageViewModel(IUserService userService, IPositionService positionService, IApplicationService applicationService)
    {
        _userService = userService;
        _positionService = positionService;
        _applicationService = applicationService;
        LoadMockData(); // Replace with LoadData(userId) when integrated with real backend
    }

    private void LoadMockData()
    {
        AppliedPositions = new ObservableCollection<Position>
        {
            new Position
            {
                Id = 1,
                Title = "Junior Software Developer",
                Level = PositionLevel.EntryLevel,
                YearsRequired = new PositionYearsRequired { From = 0, To = 2 },
                Type = PositionType.OnSite,
                Hours = 8
            },
            new Position
            {
                Id = 2,
                Title = "Software QA Engineer",
                Level = PositionLevel.EntryLevel,
                YearsRequired = new PositionYearsRequired { From = 1, To = 3 },
                Type = PositionType.Remote,
                Hours = 8
            }
        };
    }

    public async Task<WorkerProfilePageViewModel> LoadData(int? userId)
    {
        if (userId is null)
        {
            return this;
        }
        User = _userService.GetUser(new User { Id = (int)userId }) ?? _user;
        var applications = await _applicationService.GetAllApplications((int)userId, 0, "", "", "", "", "");
        foreach (var application in applications)
        {
            var position = _positionService.GetPosition(new Position { Id = (int)application.PositionId });
            AppliedPositions.Add(position);
        }
        return this;
    }

    public string FirstName
    {
        get => _user.Name.Split(' ')[0];
        set
        {
            var parts = _user.Name.Split(' ');
            _user.Name = value + (parts.Length > 1 ? " " + parts[1] : "");
            OnPropertyChanged();
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string LastName
    {
        get => _user.Name.Split(' ').Length > 1 ? _user.Name.Split(' ')[1] : string.Empty;
        set
        {
            var parts = _user.Name.Split(' ');
            _user.Name = (parts.Length > 0 ? parts[0] + " " : "") + value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _birthDate = "04.05.2003";
    public string BirthDate
    {
        get => _birthDate;
        set { _birthDate = value; OnPropertyChanged(); }
    }

    private string _phoneNumber = "0888440107";
    public string PhoneNumber
    {
        get => _phoneNumber;
        set { _phoneNumber = value; OnPropertyChanged(); }
    }

    public string Email
    {
        get => _user.Email;
        set { _user.Email = value; OnPropertyChanged(); }
    }

    private string _linkedinUrl = "https://aaaaaaaaaaaaaaa.com";
    public string LinkedInUrl
    {
        get => _linkedinUrl;
        set { _linkedinUrl = value; OnPropertyChanged(); }
    }

    private string _otherSocialUrl = "https://bbbbbbbbbbb.com";
    public string OtherSocialUrl
    {
        get => _otherSocialUrl;
        set { _otherSocialUrl = value; OnPropertyChanged(); }
    }

    private string _cvFilename = "harizmatichen.pdf";
    public string CVFilename
    {
        get => _cvFilename;
        set { _cvFilename = value; OnPropertyChanged(); }
    }

    private string _profileImage = "image1.png";
    public string ProfileImage
    {
        get => _profileImage;
        set { _profileImage = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Position> _appliedPositions = new();
    public ObservableCollection<Position> AppliedPositions
    {
        get => _appliedPositions;
        set { _appliedPositions = value; OnPropertyChanged(); }
    }

    public User User
    {
        get => _user;
        set { _user = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
