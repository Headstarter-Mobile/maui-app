using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Headstarter.Protos;
using Headstarter.Services;

namespace Headstarter.ViewModels;

public class SearchOfferPageViewModel : INotifyPropertyChanged
{
    private readonly IPositionService _positionService;

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<Position> NewPositions { get; set; } = new();

    public string SearchText { get; set; } = "";

    public List<string> Levels { get; } = new() { "Всичко", "Entry", "Internship", "Associate", "Executive", "Director", "Mid-Senior" };
    public List<string> Experiences { get; } = new() { "Всичко", "0 - 1", "2 - 4", "5 - 9", "10 +" };
    public List<string> Locations { get; } = new() { "Всички", "София", "Пловдив", "Варна", "Велико Търново" }; // Add more as needed
    public List<string> Hours { get; } = new() { "Всичко", "2 часа", "4 часа", "6 часа", "8 часа" };
    public List<string> Types { get; } = new() { "Всичко", "Присъствен", "Дистанционен", "Комбиниран" };

    private string _selectedLevel;
    public string SelectedLevel
    {
        get => _selectedLevel;
        set { _selectedLevel = value; OnPropertyChanged(); Filter(); }
    }

    private string _selectedExperience;
    public string SelectedExperience
    {
        get => _selectedExperience;
        set { _selectedExperience = value; OnPropertyChanged(); Filter(); }
    }

    private string _selectedLocation;
    public string SelectedLocation
    {
        get => _selectedLocation;
        set { _selectedLocation = value; OnPropertyChanged(); Filter(); }
    }

    private string _selectedHours;
    public string SelectedHours
    {
        get => _selectedHours;
        set { _selectedHours = value; OnPropertyChanged(); Filter(); }
    }

    private string _selectedType;
    public string SelectedType
    {
        get => _selectedType;
        set { _selectedType = value; OnPropertyChanged(); Filter(); }
    }

    public ICommand SearchCommand { get; }

    public SearchOfferPageViewModel(IPositionService positionService)
    {
        _positionService = positionService;
        SearchCommand = new Command(Filter);

        LoadPositions();
    }

    private async void LoadPositions()
    {
        var result = await _positionService.GetAllPositions(new Position());
        NewPositions.Clear();
        foreach (var position in result)
            NewPositions.Add(position);
    }

    private async void Filter()
    {
        Position filter = new Position()
        {
            Title = SearchText,
            Status = PositionStatus.Open,
        };
        {
            var level = PositionLevelFromString();
            if (level != null) filter.Level = (PositionLevel)level;
        }
        {
            var experience = PositionYearsRequiredFromString();
            if (experience != null) filter.YearsRequired = (PositionYearsRequired)experience;
        }
        {
            var hours = PositionHoursFromString();
            if (hours != null) filter.Hours = (int)hours;
        }
        {
            var types = PositionTypesFromString();
            if (types != null) filter.Type = (PositionType)types;
        }
        var result = await _positionService.GetAllPositions(filter, 10);
        NewPositions.Clear();
        foreach (var position in result)
            NewPositions.Add(position);
    }

    private PositionType? PositionTypesFromString()
    {
        switch(SelectedType)
        {
            case "Присъствен": return PositionType.OnSite;
            case "Дистанционен": return PositionType.Remote;
            case "Комбиниран": return PositionType.Hybrid;
            default: return null;
        }
    }

    private PositionLevel? PositionLevelFromString()
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
                return null;
        }
    }

    private PositionYearsRequired? PositionYearsRequiredFromString()
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
                return null;
        }
    }
    private int? PositionHoursFromString()
    {
        switch (SelectedHours)
        {
            case "2 часа": return 10;
            case "4 часа": return 20;
            case "6 часа": return 30;
            case "8 часа": return 40;
            default:
                return null;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
