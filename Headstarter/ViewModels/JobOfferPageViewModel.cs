using Headstarter.Protos;
using Headstarter.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Headstarter.ViewModels;

public class JobOfferPageViewModel
{
    private readonly IPositionService _positionService;
    private readonly ICompanyService _companyService;

    private Position _position;
    private Company _company;

    public event PropertyChangedEventHandler PropertyChanged;

    public JobOfferPageViewModel(IPositionService positionService, ICompanyService companyService)
    {
        _positionService = positionService;
        _companyService = companyService;
        ApplyCommand = new Command(OnApply);
        OpenExternalLinkCommand = new Command(OpenExternalLink);
    }

    public async Task<JobOfferPageViewModel> SetDummyData()
    {
        _position = new Position
        {
            Id = 101,
            Title = "Junior Software Developer",
            Description = @"Your Responsibilities:
- Analyze business requirements for new business information reports and map them to the respective database objects, considering the business data dictionary;
- Develop, test and validate the respective SQL code for extraction of the requested business information and validate the resultative dataset with the business owner;
- Implement the validated dataset with the respective reporting solution and provide the requested report for testing and final validation by the business owner;
- Participate in the production release of developed reports, maintain production reports and process the respective change requests according to the established processes.

Requirements:
- Working experience with SQL databases, BI and reporting;
- Self-motivated person with a high degree of independence;
- Good communication skills and a service-oriented behavior.

Advantages:
- Working experience with Oracle database, Oracle BI and other Oracle products;
- Working experience with JasperReports Server and similar reporting platforms;
- Knowledge and experience with Java, JavaScript, Spring Boot, Thymeleaf, Quarkus.

We offer:
- Opportunities for long-term professional realization and development in an established, stable company;
- Work in a well-built infrastructure environment;
- Opportunities for learning and development;
- Excellent work environment;
- A team of proven professionals;
- Attractive benefits package.",
            Status = PositionStatus.Open,
            ExternalApplicationLink = "https://borislaviainc.com/apply-now",
            CreatedAt = "12.12.2022",
            UpdatedAt = "13.12.2022",
            PublishedAt = "12.12.2022",
            ExpiresAt = "31.12.2025",
            Level = PositionLevel.MidSeniorLevel,
            YearsRequired = new PositionYearsRequired { From = 0, To = 2 },
            Hours = 8,
            Type = PositionType.Remote,
            CompanyId = 1,
            Offices = { new Office {
                Id = 1,
                Name = "Main Office",
                Address = "123 Tech St",
                Location = "Велико Търново",
                Description = "Headquarters",
                CompanyId = 1,
                CreatedAt = "2022-01-01",
                UpdatedAt = "2022-01-01"
            }}
        };
        _company = new Company
        {
            Id = 1,
            Name = "Borislavia Inc",
            Description = "Tech company focused on innovation",
            Logo = "borislavia_logo.png",
            Website = "https://borislaviainc.com",
            CreatedAt = "2022-01-01",
            UpdatedAt = "2023-01-01"
        };
        return this;
    }

    public async Task<JobOfferPageViewModel> ForPosition(int positionId)
    {
        var list = (await _positionService.GetAllPositions(new Position { Id = positionId }, 1));
        if (list.Count == 0)
            throw new ArgumentNullException(nameof(positionId));
        _position = list.First();
        if (_position == null)
            throw new ArgumentNullException(nameof(_position));
        _company = _companyService.GetCompany(new Company { Id = _position.CompanyId });
        if (_company == null)
            throw new ArgumentNullException(nameof(_company));
        return this;
    }

    public string Title => _position.Title;
    public string Description => _position.Description;
    public string CompanyName => _company.Name;
    public string CreatedAt => _position.CreatedAt;
    public string Level => _position.Level.ToString();
    public string YearsExperience => $"{_position.YearsRequired.From} - {_position.YearsRequired.To}";
    public string Location => _position.Offices.Count > 0 ? _position.Offices[0].Location : "";
    public string Hours => _position.Hours + " часа";
    public string Type => _position.Type.ToString();
    public bool ExternalLinkMissing => string.IsNullOrEmpty(_position.ExternalApplicationLink);


    public ICommand ApplyCommand { get; }
    public ICommand OpenExternalLinkCommand { get; }

    private async void OnApply()
    {
        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Кандидатстване", $"Кандидатствахте за позиция: {Title}", "OK");
    }
    private void OpenExternalLink()
    {
        if (!string.IsNullOrEmpty(_position.ExternalApplicationLink))
        {
            try
            {
                Launcher.OpenAsync(new Uri(_position.ExternalApplicationLink));
            }
            catch (Exception)
            {
                Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Грешка", "Линкът не може да бъде отворен.", "OK");
            }
        }
        else
        {
            Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Липсва линк", "Няма предоставен външен линк за кандидатстване.", "OK");
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
