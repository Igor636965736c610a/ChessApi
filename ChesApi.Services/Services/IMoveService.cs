using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public interface IMoveService
    {
        GameStatus Move(MoveType moveType, Vector2 newVector2, Vector2 oldVector2, Guid userId);
    }
}
