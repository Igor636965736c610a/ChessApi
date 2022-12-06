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
        private readonly IFigureRepository _figureRepository;
        public StandartMove(IFigureRepository figureRepository)
        {
            _figureRepository = figureRepository;
        }

        public GameStatus Move(Vector2 newVector2, Figure figure, Board board, out Figure? figureToDelete)
        {
            IEnumerable<Figure> enemyFigures = _figureRepository.GetFiguresByColor(board, !figure.WhiteColor);
            List<Figure> attackingFigures = enemyFigures.SkipWhile(x => x.Vector2.X == newVector2.X && x.Vector2.Y == newVector2.Y).ToList();
            figureToDelete = board.FieldsStatus[newVector2.X, newVector2.Y];
            if (!figure.ChcekLegalMovement(board, newVector2, attackingFigures))
                return GameStatus.IllegalMove;

            return GameStatus.IsGaming;
        }
    }
}
