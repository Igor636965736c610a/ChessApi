using ChesApi.Infrastructure.MoveTypeStrategy;
using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.Utils;
using Chess.Core.Repo.Game;
using Chess.Core.Repo.UserRepository;

namespace ChesApi.Infrastructure.Services
{
    public class MoveService : IMoveService
    {
        private readonly IFigureRepository _figureRepository;
        private readonly IStrategyFactory<IStrategy> _strategyFactor;
        public MoveService
            (IFigureRepository figureRepository, IStrategyFactory<IStrategy> strategyFactory)
        {
            _figureRepository = figureRepository;
            _strategyFactor = strategyFactory;
        }

        public GameStatus Move(MoveType moveType, Vector2 newVector2, Vector2 oldVector2, LiveGame liveGame)
        {
            if(liveGame is null)
                throw new Exception("404 sesion");

            var figure = _figureRepository.GetFigure(liveGame.Board, oldVector2);
            if(figure is null)
                throw new NullReferenceException();

            var board = liveGame.Board;
            if (newVector2.X < board.XMin && newVector2.X >= board.XMax && newVector2.Y < board.YMin && newVector2.Y >= board.YMax)
                throw new InvalidOperationException();

            if (oldVector2.X == newVector2.X && oldVector2.Y == newVector2.Y)
                throw new InvalidOperationException();

            var strategy = _strategyFactor.GetStrategy(moveType.ToString());

            var legal = strategy.Move(newVector2, figure, board);
            if(!legal)
                throw new InvalidOperationException();
            if (figure.FigureType == FigureType.Pown && Math.Abs(newVector2.Y - figure.Vector2.Y) == 2)
                board.EnPassant = new EnPassant(true, new Vector2(newVector2.Y - Math.Sign(newVector2.Y - figure.Vector2.Y), newVector2.X));
            else
                board.EnPassant = new EnPassant();
            var figureToDelete = board.FieldsStatus[newVector2.X, newVector2.Y];
            if (figureToDelete is not null)
                _figureRepository.RemoveFigure(board, figureToDelete);

            figure.SetNewPosition(newVector2);
            board.FieldsStatus[oldVector2.X, oldVector2.Y] = null;
            board.FieldsStatus[newVector2.X, newVector2.Y] = figure;

            List<Figure> enemyFigures = _figureRepository.GetFiguresByColor(board, !figure.WhiteColor).ToList();
            var enemyKing = _figureRepository.GetKing(board, !figure.WhiteColor);
            var king = _figureRepository.GetKing(board, figure.WhiteColor);
            List<Figure> figures = board.Figures.Where(x => x.WhiteColor == figure.WhiteColor && x.FigureType != FigureType.King)
                .ToList();
            List<Figure> attackingFigures = new();
            foreach(var f in figures)
            {
                if(f.ChcekLegalMovement(board, enemyKing.Vector2, enemyFigures))
                    attackingFigures.Add(f);
            }
            if(attackingFigures.Count > 0)
            {
                if(CheckCheckmate(board, enemyKing, king, attackingFigures))
                    return liveGame.WhiteColor ? GameStatus.WhiteCheckMate : GameStatus.BlackCheckMate;
                else
                    return liveGame.WhiteColor ? GameStatus.WhiteCheck : GameStatus.BlackCheck;
            }

            return GameStatus.IsGaming;
        }

        private bool CheckCheckmate(Board board, Figure enemyKing, Figure king, List<Figure> attackingFigures)
        {
            var kingLegalMovement = king.ShowLegalMovement(board, attackingFigures);
            foreach(var k in kingLegalMovement)
            {
                if(k == true)
                    return false;
            }

            if(attackingFigures.Count() > 1)
            {
                if(attackingFigures.Any(x => x.FigureType == FigureType.Knight))
                    return true;

                if(attackingFigures.Any(x => x.FigureType == FigureType.Rook) && attackingFigures
                    .Any(x => x.FigureType == FigureType.Bishop))
                    return true;

                List<Vector2> attackDirections = new();
                foreach(var f in attackingFigures)
                {
                    attackDirections.Add(new Vector2(Math.Sign(enemyKing.Vector2.X - f.Vector2.X), Math.Sign(enemyKing.Vector2.Y - f.Vector2.Y)));
                }
                if (!attackDirections.All(x => x.X == attackDirections.First().X && x.Y == attackDirections.First().Y))
                    return true;
            }
            var defendingFigures = _figureRepository.GetFiguresByColor(board, enemyKing.WhiteColor)
                .SkipWhile(x => x.FigureType == FigureType.King);
            var figure = attackingFigures.OrderBy(x => Math.Abs(enemyKing.Vector2.X + enemyKing.Vector2.Y - x.Vector2.X + x.Vector2.Y)).First();
            var enemyFigures = _figureRepository.GetFiguresByColor(board, !king.WhiteColor)
                .Where(x => !attackingFigures.Contains(x));
            var direction = new Vector2(enemyKing.Vector2.X - figure.Vector2.X, enemyKing.Vector2.Y - figure.Vector2.Y);
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));
            var current = figure.Vector2;

            while ((current.X != enemyKing.Vector2.X) && (current.Y != enemyKing.Vector2.Y))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (UtilsMethods.CheckCover(current, defendingFigures, attackingFigures, board))
                    return true;
            }
            return false;
        }
    }
}
