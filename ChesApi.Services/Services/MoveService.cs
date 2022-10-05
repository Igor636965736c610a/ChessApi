using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain;
using Chess.Core.Domain.DefaultConst;
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
        private readonly IUserInGameRepository _userInGameRepository;
        private readonly IFigureRepository _figureRepository;
        public MoveService
            (IUserInGameRepository userInGameRepository, IFigureRepository figureRepository)
        {
            _userInGameRepository = userInGameRepository;
            _figureRepository = figureRepository;
        }

        public GameStatus Move(Vector2 newVector2, Vector2 oldVector2, Guid userId)  //userId from JWT
        {
            //validations and needed properties
            GameStatus gameStatus = GameStatus.IsGaming;
            if (newVector2.X < Board.X && newVector2.X >= 0 && newVector2.Y < Board.Y && newVector2.Y >= 0)
                throw new Exception("X and Y must be 0 - 7");

            var user = _userInGameRepository.GetUserById(userId);
            if (user is null)
                throw new NullReferenceException();

            var liveGame = user.LiveGame;
            if(liveGame is null)
                throw new Exception("404 sesion");

            if(liveGame.IsGaming == false)
                throw new InvalidOperationException();

            var figure = _figureRepository.GetFigure(liveGame, oldVector2);
            if(figure is null)
                throw new NullReferenceException();

            if (user.WhiteColor != liveGame.WhiteColor)
                throw new InvalidOperationException();

            if (oldVector2.X == newVector2.X && oldVector2.Y == newVector2.Y)
                throw new InvalidOperationException();

            IEnumerable<Figure> enemyFigures = _figureRepository.GetFiguresByColor(liveGame, !figure.WhiteColor);
            IEnumerable<Figure> figures = _figureRepository.GetFiguresByColor(liveGame, figure.WhiteColor);
            Figure king = _figureRepository.GetKing(liveGame, figure.WhiteColor);
            Figure enemyKing = _figureRepository.GetKing(liveGame, !figure.WhiteColor);

            //Move
            var attackingFigures = enemyFigures.SkipWhile(x => x.Vector2.X == newVector2.X && x.Vector2.Y == newVector2.Y);
            if (UtilsMethods.CheckRevealAttack(figure, liveGame.FieldsStatus, attackingFigures, king.Vector2))
                throw new InvalidOperationException();
            if(!figure.ChcekLegalMovement(liveGame, newVector2))
                throw new InvalidOperationException();
            figure.SetNewPosition(newVector2);
            
            Figure? toRemoveFigure = enemyFigures.FirstOrDefault(x => x.Vector2.X == newVector2.X && x.Vector2.Y == newVector2.Y);
            if (toRemoveFigure is not null)
            {
                _figureRepository.RemoveFigure(liveGame, toRemoveFigure);
            }
            UtilsMethods.SetAttackingFigures(figures, liveGame.FieldsStatus, enemyKing.Vector2);
            if (liveGame.Figures.Any(x => x.IsAttacking == true) && CheckCheckmate(liveGame, enemyKing, king))
            {
                if (figure.WhiteColor)
                    gameStatus = GameStatus.WhiteMat;
                else
                    gameStatus = GameStatus.BlackMat;
            }
            liveGame.WhiteColor = !figure.WhiteColor;
            user.WhiteColor = !figure.WhiteColor;
            foreach(var f in liveGame.Figures)
            {
                f.IsAttacking = false;
            }
            return gameStatus;
        }

        private bool CheckCheckmate(LiveGame liveGame, Figure enemyKing, Figure king)
        {
            //sprawdzenie legalności ruchow krola


            var attackingFigures = _figureRepository.GetFiguresIsAttacking(liveGame, king.WhiteColor);
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
            var defendingFigures = _figureRepository.GetFiguresByColor(liveGame, enemyKing.WhiteColor)
                .SkipWhile(x => x.FigureType == FigureType.King);
            var figure = attackingFigures.OrderBy(x => Math.Abs(enemyKing.Vector2.X + enemyKing.Vector2.Y - x.Vector2.X + x.Vector2.Y)).First();
            var enemyFigures = _figureRepository.GetFiguresByColor(liveGame, !king.WhiteColor).SkipWhile(x => x.IsAttacking == true);
            var direction = new Vector2(enemyKing.Vector2.X - figure.Vector2.X, enemyKing.Vector2.Y - figure.Vector2.Y);
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));
            var current = figure.Vector2;

            while ((current.X != enemyKing.Vector2.X) && (current.Y != enemyKing.Vector2.Y))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (UtilsMethods.CheckCover(current, defendingFigures, attackingFigures, liveGame.FieldsStatus, king.Vector2))
                    return true;
            }
            return false;
        }
    }
}
