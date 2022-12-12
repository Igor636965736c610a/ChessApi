using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Repo.Game;
using Chess.Core.Repo.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.MoveTypeStrategy.MoveTypes
{
    public class StandartMove : IStrategy
    {
        public StandartMove()
        {
        }

        public bool Move(Vector2 newVector2, Figure figure, Board board)
        {
            if(figure.FigureType == FigureType.Pown && (newVector2.Y == 7 || newVector2.Y == 0)) 
                return false;

            List<Figure> enemyFigures = board.Figures
                .Where(x => x.WhiteColor == !figure.WhiteColor)
                .ToList();
            return !figure.ChcekLegalMovement(board, newVector2, enemyFigures);
        }
    }
}
