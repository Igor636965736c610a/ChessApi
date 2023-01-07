using ChesApi.Infrastructure.MoveTypeStrategy;
using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.Utils;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IFigureRepository _figureRepository;
        private readonly IStrategyFactory<IStrategy> _strategyFactor;
        public GameService(IFigureRepository figureRepository ,IStrategyFactory<IStrategy> strategyFactory)
        {
            _figureRepository = figureRepository;
            _strategyFactor = strategyFactory;
        }
        public GameStatus Move(MoveType moveType, Vector2 newVector2, Vector2 oldVector2, LiveGame liveGame)
        {
            if (liveGame is null)
                throw new Exception("404 sesion");

            var figure = _figureRepository.GetFigure(liveGame.Board, oldVector2);
            if (figure is null)
                throw new NullReferenceException("Wybierz figure");

            if (figure.WhiteColor != liveGame.WhiteColor)
                throw new NullReferenceException("Wybierz swoja figure");

            var board = liveGame.Board;
            if (UtilsMethods.ValidateVetor2(oldVector2, board) && UtilsMethods.ValidateVetor2(newVector2, board))
                throw new InvalidOperationException();

            if (oldVector2.X == newVector2.X && oldVector2.Y == newVector2.Y)
                throw new InvalidOperationException();

            var strategy = _strategyFactor.GetStrategy(moveType.ToString());

            var legalMoveType = strategy.Move(newVector2, figure, board);
            if (!legalMoveType)
                throw new InvalidOperationException();

            var enemyKing = _figureRepository.GetKing(board, !figure.WhiteColor);

            var attackingFigures = board.Figures
                .Where(x => x.WhiteColor == figure.WhiteColor && x.FigureType != FigureType.King
                 && x.CheckLegalMovement(board, board.FieldsStatus, enemyKing.Vector2, board.Figures.Where(z => z.WhiteColor != figure.WhiteColor)
                ,null))
                .ToList();

            if (attackingFigures.Count <= 0)
                return GameStatus.IsGaming;
            if (CheckCheckmate(board, enemyKing, attackingFigures))
                return liveGame.WhiteColor ? GameStatus.WhiteCheckMate : GameStatus.BlackCheckMate;
            return liveGame.WhiteColor ? GameStatus.WhiteCheck : GameStatus.BlackCheck;

        }

        private static bool CheckCheckmate(Board board, Figure checkKing, IEnumerable<Figure> attackingFigures)
        {
            var dirs = checkKing.Dirs;
            var enemyFigures = board.Figures.Where(x => x.WhiteColor != checkKing.WhiteColor);
            if (dirs.Any(x => !UtilsMethods.ValidateVetor2(new Vector2(checkKing.Vector2.X + x.X, checkKing.Vector2.Y + x.Y),
                board)
                && checkKing.CheckLegalMovement(board, board.FieldsStatus,
                new Vector2(checkKing.Vector2.X + x.X, checkKing.Vector2.Y + x.Y), enemyFigures, null)))
                return false;

            if (attackingFigures.Count() > 1)
            {
                if (attackingFigures.Any(x => x.FigureType == FigureType.Knight))
                    return true;

                if (attackingFigures.Any(x => x.FigureType == FigureType.Rook) && attackingFigures
                    .Any(x => x.FigureType == FigureType.Bishop))
                    return true;

                var attackDirections = attackingFigures
                    .Select(x => new Vector2(Math.Sign(checkKing.Vector2.X - x.Vector2.X), Math.Sign(checkKing.Vector2.Y - x.Vector2.Y)));

                if (!attackDirections.All(x => x.X == attackDirections.First().X && x.Y == attackDirections.First().Y))
                    return true;
            }
            var defendingFiguresToCheckCover = board.Figures.Where(x => x.WhiteColor == checkKing.WhiteColor && x.FigureType != FigureType.King);
            var firsInTheRowFigure = attackingFigures
                .OrderBy(x => Math.Abs(checkKing.Vector2.X + checkKing.Vector2.Y - x.Vector2.X + x.Vector2.Y))
                .First();
            var direction = new Vector2(firsInTheRowFigure.Vector2.X - checkKing.Vector2.X, firsInTheRowFigure.Vector2.Y - checkKing.Vector2.Y);
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));
            var current = checkKing.Vector2;

            while (!UtilsMethods.CompareVector2(current, firsInTheRowFigure.Vector2))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (UtilsMethods.CheckCover(current, defendingFiguresToCheckCover, enemyFigures, board, checkKing))
                    return false; 
            }
            return true;
        }
    }
}
