using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.MoveTypeStrategy.MoveTypes
{
    public class RookPromotion : IStrategy
    {
        public bool Move(Vector2 newVector2, Figure figure, Board board)
        {
            if (figure.FigureType != FigureType.Pown && (newVector2.Y != 7 || newVector2.Y != 0))
                return false;

            if (!UtilsMove.BaseMove(newVector2, figure, board))
                return false;

            var rook = new Rook(figure.WhiteColor, newVector2, false);
            UtilsMove.Promotion(newVector2, figure, board, rook);

            return true;
        }
    }
}
