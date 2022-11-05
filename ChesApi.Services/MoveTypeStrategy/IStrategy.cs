using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.MoveTypeStrategy
{
    public interface IStrategy
    {
        GameStatus Move(Vector2 newVector2, Figure figure, Board board, out Figure? figureToDelete);
    }
}
