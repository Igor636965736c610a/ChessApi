using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.MoveTypeStrategy.MoveTypes
{
    public class BischopPromotion : IStrategy
    {
        public GameStatus Move(Vector2 newVector2, Vector2 oldVector2, Board board, out Figure figureToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
