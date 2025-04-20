using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Headstarter.Protos;
using Headstarter.Services;

namespace Headstarter.ViewModels;

public class CreateOfferPageViewModel : INotifyPropertyChanged
{
    private readonly IPositionService _positionService;
    private readonly ICompanyService _companyService;
    private readonly IOfficeService _officeService;

    public event PropertyChangedEventHandler PropertyChanged;

    public string SearchText { get => searchText; set { searchText = value; OnPropertyChanged(); } }
    public List<string> Levels { get; } = new() { "Entry", "Internship", "Associate", "Executive", "Director", "Mid-Senior" };
    public List<string> Experiences { get; } = new() { "0 - 1", "2 - 4", "5 - 9", "10 +" };
    public List<string> Locations { get; } = new() { "София", "Пловдив", "Варна", "Велико Търново" }; // Add more as needed
    public List<string> Hours { get; } = new() { "2 часа", "4 часа", "6 часа", "8 часа" };
    public List<string> Types { get; } = new() { "Присъствен", "Дистанционен", "Комбиниран" };

    public List<string> Visibilities { get; } = new() { "Тестова", "Публична", "Изтекла" };

    private string _selectedUserType;
    public string SelectedUserType
    {
        get => _selectedUserType;
        set { _selectedUserType = value; OnPropertyChanged(); }
    }

    private string _selectedLevel;
    public string SelectedLevel
    {
        get => _selectedLevel;
        set { _selectedLevel = value; OnPropertyChanged(); }
    }

    private string _selectedExperience;
    public string SelectedExperience
    {
        get => _selectedExperience;
        set { _selectedExperience = value; OnPropertyChanged(); }
    }

    private string _selectedLocation;
    public string SelectedLocation
    {
        get => _selectedLocation;
        set { _selectedLocation = value; OnPropertyChanged(); }
    }

    private string _selectedHours;
    public string SelectedHours
    {
        get => _selectedHours;
        set { _selectedHours = value; OnPropertyChanged(); }
    }

    private string _selectedType;
    private Company company;
    private IEnumerable<Office> offices;

    public string SelectedType
    {
        get => _selectedType;
        set { _selectedType = value; OnPropertyChanged(); }
    }

    public Company Company
    {
        get => company;
        set { company = value; OnPropertyChanged(); }
    }

    private Position position;
    private string searchText;

    public ICommand SearchCommand { get; }

    public CreateOfferPageViewModel(IPositionService positionService, ICompanyService companyService, IOfficeService officeService)
    {
        _positionService = positionService;
        _companyService = companyService;
        _officeService = officeService;
        Task.Run(() => LoadCompany());
    }

    private async void LoadCompany()
    {
        Company = _companyService.GetCompany(new Company
        {
            Id = SessionService.Instance.CurrentUser.CompanyId
        });
        offices = await _officeService.GetAllOffices(new Office
        {
            CompanyId = SessionService.Instance.CurrentUser.CompanyId
        });
    }

    private PositionType PositionTypesFromString()
    {
        switch (SelectedType)
        {
            case "Присъствен": return PositionType.OnSite;
            case "Дистанционен": return PositionType.Remote;
            case "Комбиниран": return PositionType.Hybrid;
            default: return PositionType.OnSite;
        }
    }

    private PositionLevel PositionLevelFromString()
    {

        switch (SelectedLevel)
        {
            case "Entry": return PositionLevel.EntryLevel;
            case "Internship": return PositionLevel.Internship;
            case "Associate": return PositionLevel.Associate;
            case "Executive": return PositionLevel.Executive;
            case "Director": return PositionLevel.Director;
            case "Mid-Senior": return PositionLevel.MidSeniorLevel;
            default:
                return PositionLevel.EntryLevel;
        }
    }

    private PositionYearsRequired PositionYearsRequiredFromString()
    {
        switch (SelectedExperience)
        {
            case "0 - 1":
                return new PositionYearsRequired()
                {
                    From = 0,
                    To = 1
                };
            case "2 - 4":
                return new PositionYearsRequired()
                {
                    From = 2,
                    To = 4
                };
            case "5 - 9":
                return new PositionYearsRequired()
                {
                    From = 5,
                    To = 9
                };
            case "10 +":
                return new PositionYearsRequired()
                {
                    From = 10,
                };
            default:
                return new PositionYearsRequired()
                {
                    From = 0,
                    To = 1
                };
        }
    }
    private int PositionHoursFromString()
    {
        switch (SelectedHours)
        {
            case "2 часа": return 10;
            case "4 часа": return 20;
            case "6 часа": return 30;
            case "8 часа": return 40;
            default:
                return 10;
        }
    }
    private async void Create()
    {
        Position newPosition = new Position()
        {
            Title = SearchText,
            Status = VisibilityFromString(SelectedUserType),
            Level = PositionLevelFromString(),
            YearsRequired = PositionYearsRequiredFromString(),
            Hours = PositionHoursFromString(),
            Type = PositionTypesFromString(),
            CompanyId = SessionService.Instance.CurrentUser.CompanyId
        };
        newPosition.Offices.AddRange(offices);
        _positionService.CreatePosition(newPosition);
    }
        private PositionStatus VisibilityFromString(string visibility)
    {
        return visibility switch
        {
            "Тестова" => PositionStatus.Draft,
            "Публична" => PositionStatus.Open,
            "Изтекла" => PositionStatus.Expired,
            _ => PositionStatus.Open,
        };
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
