using Headstarter.Protos;
using Headstarter.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace Headstarter.ViewModels;

public class CompanyPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private readonly ICompanyService _companyService;
    private readonly IOfficeService _officeService;
    private readonly IPositionService _positionService;

    private IEnumerable<Office> offices;
    private Company company;
    private IEnumerable<Position> positions;

    public Protos.Company Company
    {
        get => company; 
        private set
        {
            if (company != value)
            {
                company = value;
                OnPropertyChanged(nameof(Company));
            }
        }
    }

    public IEnumerable<Protos.Office> Offices
    { 
        get => offices; 
        private set
        {
            if (offices != value)
            {
                offices = value;
                OnPropertyChanged(nameof(Offices));
            }
        }
    }

    public IEnumerable<Protos.Position> Positions
    {
        get => positions;
        private set
        {
            if (positions != value)
            {
                positions = value;
                OnPropertyChanged(nameof(Positions));
            }
        }
    }

    public CompanyPageViewModel(ICompanyService companyService, IOfficeService officeService, IPositionService positionService)
    {
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _officeService = officeService ?? throw new ArgumentNullException(nameof(officeService));
        _positionService = positionService ?? throw new ArgumentNullException(nameof(positionService));
    }

    private async Task LoadOfficesForCompany(int companyId)
    {
        var offices = await _officeService.GetAllOffices(new Protos.Office
        {
            CompanyId = companyId
        });
        if (offices == null)
            Offices = new List<Protos.Office>();
        else
            Offices = offices;
    }

    private async Task LoadPositionsForCompany(int companyId)
    {
        var positions = await _positionService.GetAllPositions(new Protos.Position
        {
            CompanyId = companyId
        });
        if (positions == null)
            Positions = new List<Protos.Position>();
        else
            Positions = positions;
    }

    public async Task<CompanyPageViewModel> ForCompany(int id)
    {
        Company = _companyService.GetCompany(new Protos.Company
        {
            Id = id
        });

        if (Company == null)
            return this;

        await LoadOfficesForCompany(id);

        await LoadPositionsForCompany(id);

        return this;
    }
    
}
