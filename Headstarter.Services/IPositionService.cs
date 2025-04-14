using Headstarter.Protos;

namespace Headstarter.Services
{
    public interface IPositionService
    {
        Position GetPosition(Position position);
        Task<ICollection<Position>> GetAllPositions(Position filters, int? limit = null);
        Position CreatePosition(Position position);
        Position UpdatePosition(Position oldPosition, Position newPosition);
        Position DeletePosition(Position position);
    }
}
