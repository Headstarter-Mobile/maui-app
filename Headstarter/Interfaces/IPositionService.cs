using Headstarter.Protos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Headstarter.Interfaces
{
    internal interface IPositionService
    {
        Position GetPosition(Position position);
        Task<ICollection<Position>> GetAllPositions();
        Position CreatePosition(Position position);
        Position UpdatePosition(Position oldPosition, Position newPosition);
        Position DeletePosition(Position position);
    }
}
