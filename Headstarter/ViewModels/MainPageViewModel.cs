using Headstarter.Services;

namespace Headstarter.ViewModels;

public class MainPageViewModel
{
    private readonly IPositionService _positionService;

    public IEnumerable<Protos.Position> NewPositions { get; private set; }

    public MainPageViewModel()
    {
        _positionService = Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IPositionService>();
        Task.Run(() => LoadData()).Wait();
    }
    private async Task LoadData()
    {
        NewPositions = await _positionService.GetAllPositions(new Protos.Position(), 5);
    }
}
