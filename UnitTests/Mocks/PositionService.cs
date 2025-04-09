using Headstarter.Interfaces;
using Headstarter.Protos;

namespace UnitTests.Mocks
{
    internal class PositionService : IPositionService
    {
        public Position CreatePosition(Position position)
        {
            throw new NotImplementedException();
        }

        public Position DeletePosition(Position position)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Position>> GetAllPositions()
        {
            throw new NotImplementedException();
        }

        public Position GetPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public Position UpdatePosition(Position oldPosition, Position newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
