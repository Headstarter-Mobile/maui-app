using System.ComponentModel;
using System.Runtime.CompilerServices;
using Headstarter.Protos;
using Headstarter.Services;

namespace Headstarter.ViewModels;

public class RecruiterProfilePageViewModel : INotifyPropertyChanged
{
    private User _user = new()
    {
        Name = "Boris Vlahov",
        Email = "bobovlahov@gmail.com",
        Type = UserRole.Recruiter,
        CompanyId = 1
    };

    private Company _company = new()
    {
        Id = 1,
        Name = "Borislavia Inc",
        Logo = "image1.png",
        Website = "https://Borislavia.com",
        Description = "In a world where time moves fast, we seek moments that last. A smile, a kind word, or a shared laugh can make all the difference. Life is a journey, full of surprises and lessons. Embrace each day with hope, cherish loved ones, and never stop dreaming. The best stories are yet to be written."
    };

    private ICollection<Position> _positions = [
        new Position
            {
                Id = 1,
                Title = "Junior Software Developer",
                Description = "Develop, test, and deploy web applications with .NET MAUI.",
                CompanyId = 1,
                Status = PositionStatus.Open,
                Level = PositionLevel.EntryLevel,
                YearsRequired = new PositionYearsRequired { From = 0, To = 2 },
                Hours = 8,
                Type = PositionType.Remote,
                ExternalApplicationLink = "https://borislavia.com/apply/1",
                CreatedAt = "2023-01-10T10:00:00Z",
                UpdatedAt = "2023-01-15T14:00:00Z",
                PublishedAt = "2023-01-12T09:00:00Z",
                ExpiresAt = "2025-01-01T00:00:00Z"
            },
            new Position
            {
                Id = 2,
                Title = "Mid-Level Backend Engineer",
                Description = "Work on scalable APIs and microservices with .NET Core and SQL Server.",
                CompanyId = 1,
                Status = PositionStatus.Open,
                Level = PositionLevel.Associate,
                YearsRequired = new PositionYearsRequired { From = 2, To = 4 },
                Hours = 8,
                Type = PositionType.Hybrid,
                ExternalApplicationLink = "https://borislavia.com/apply/2",
                CreatedAt = "2023-02-01T09:00:00Z",
                UpdatedAt = "2023-02-05T16:00:00Z",
                PublishedAt = "2023-02-03T08:00:00Z",
                ExpiresAt = "2025-02-01T00:00:00Z"
            },
            new Position
            {
                Id = 3,
                Title = "Software QA Engineer",
                Description = "Create and maintain test cases, perform automated and manual testing.",
                CompanyId = 1,
                Status = PositionStatus.Open,
                Level = PositionLevel.EntryLevel,
                YearsRequired = new PositionYearsRequired { From = 1, To = 3 },
                Hours = 8,
                Type = PositionType.OnSite,
                ExternalApplicationLink = "https://borislavia.com/apply/3",
                CreatedAt = "2023-03-20T10:30:00Z",
                UpdatedAt = "2023-03-22T10:45:00Z",
                PublishedAt = "2023-03-21T08:00:00Z",
                ExpiresAt = "2025-03-20T00:00:00Z"
            }
    ];

    public RecruiterProfilePageViewModel(IUserService userService, ICompanyService companyService, IPositionService positionService)
    {
        _userService = userService;
        _companyService = companyService;
        _positionService = positionService;
    }

    public async Task<RecruiterProfilePageViewModel> LoadData(User? user)
    {
        // Dummy data
        if (user is null)
            return this;
        User = user;
        Company = _companyService.GetCompany(new Company()
        {
            Id = User.CompanyId
        });
        if (Company == null)
            throw new ArgumentNullException(nameof(Company));
        Positions = await _positionService.GetAllPositions(new Position
        {
            CompanyId = Company.Id
        });
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
    private IUserService _userService;
    private ICompanyService _companyService;
    private IPositionService _positionService;

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

    public Company Company
    {
        get => _company;
        set { _company = value; OnPropertyChanged(); }
    }

    public User User
    {
        get => _user;
        set { _user = value; OnPropertyChanged(); }
    }

    public ICollection<Position> Positions
    {
        get => _positions;
        set { _positions = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
