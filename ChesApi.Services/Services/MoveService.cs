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

            if (user.FigureColor != liveGame.FigureColor)
                throw new InvalidOperationException();

            if (oldVector2.X == newVector2.X && oldVector2.Y == newVector2.Y)
                throw new InvalidOperationException();

            IEnumerable<Figure> enemyFigures;
            IEnumerable<Figure> figures;
            Figure king;
            Figure enemyKing;
            if (figure.Color == FigureColor.White)
            {
                enemyFigures = _figureRepository.GetFiguresByColor(liveGame, FigureColor.Black);
                figures = _figureRepository.GetFiguresByColor(liveGame, FigureColor.White);
                king = _figureRepository.GetKing(liveGame, FigureColor.White);
                enemyKing = _figureRepository.GetKing(liveGame, FigureColor.Black);
            }
            else
            {
                enemyFigures = _figureRepository.GetFiguresByColor(liveGame, FigureColor.White);
                figures = _figureRepository.GetFiguresByColor(liveGame, FigureColor.Black);
                king = _figureRepository.GetKing(liveGame, FigureColor.Black);
                enemyKing = _figureRepository.GetKing(liveGame, FigureColor.White);
            }

            //Move
            Figure figureColorKing = _figureRepository.GetKing(liveGame, figure.Color);
            var attackingFigures = enemyFigures.SkipWhile(x => x.Vector2.X == newVector2.X && x.Vector2.Y == newVector2.Y);
            var attackFieldsAftreMove = StaticMoveLogicMethods.CheckDiscoveredAttack(figure, liveGame.FieldsStatus, attackingFigures);
            if(!figure.ChcekLegalMovement(liveGame.FieldsStatus, newVector2, direction))
                throw new InvalidOperationException();
            if (StaticMoveLogicMethods.CheckSetNewPosition(figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                figure.SetNewPosition(newVector2);
            else
                throw new InvalidOperationException();
            Figure? toRemoveFigure = enemyFigures.FirstOrDefault(x => x.Vector2.X == newVector2.X && x.Vector2.Y == newVector2.Y);
            if (toRemoveFigure is not null)
            {
                if(figure.Color == FigureColor.White)
                    liveGame.FieldsStatus[newVector2.X, newVector2.Y].OccupiedWhiteFields = false;
                else
                    liveGame.FieldsStatus[newVector2.X, newVector2.Y].OccupiedBlackFields = false;
                _figureRepository.RemoveFigure(liveGame, toRemoveFigure);
            }

            //AfterMoveActions
            UpdateFieldsStatus(figure, liveGame.FieldsStatus, newVector2);
            var whiteKing = _figureRepository.GetKing(liveGame, FigureColor.White);
            var blackKing = _figureRepository.GetKing(liveGame, FigureColor.Black);
            var blackFigures = _figureRepository.GetFiguresByColor(liveGame, FigureColor.Black);
            var whiteFigures = _figureRepository.GetFiguresByColor(liveGame, FigureColor.White);
            bool[,] newWhiteAttackFields = StaticMoveLogicMethods.SetNewAttackFields(whiteFigures, liveGame.FieldsStatus);
            bool[,] newBlackAttackFields = StaticMoveLogicMethods.SetNewAttackFields(blackFigures, liveGame.FieldsStatus);
            UpdateWhiteAttackFieldsStatus(liveGame.FieldsStatus, newWhiteAttackFields);
            UpdateBlackAttackFielsStatus(liveGame.FieldsStatus, newBlackAttackFields);
            StaticMoveLogicMethods.SetAttackingFigures(figures, liveGame.FieldsStatus, enemyKing.Vector2);

            if (figure.Color == FigureColor.White)
            {
                if (liveGame.FieldsStatus[blackKing.Vector2.X, blackKing.Vector2.Y].AttackedWhiteFields && CheckCheckmate(liveGame, blackKing, whiteKing))
                    gameStatus = GameStatus.WhiteMat;

                liveGame.FigureColor = FigureColor.Black;
                user.FigureColor = FigureColor.Black;
            }
            if (figure.Color == FigureColor.Black)
            {
                if (liveGame.FieldsStatus[whiteKing.Vector2.X, whiteKing.Vector2.Y].AttackedBlackFields && CheckCheckmate(liveGame, whiteKing, blackKing))
                    gameStatus = GameStatus.BlackMat;

                liveGame.FigureColor = FigureColor.White;
                user.FigureColor = FigureColor.White;
            }
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

        private static void UpdateWhiteAttackFieldsStatus(FieldsStatus[,] fieldsStatus, bool[,] newFieldsStatusProperty)
        {
            for (int i = 0; i < fieldsStatus.GetLength(0); i++)
            {
                for (int y = 0; y < fieldsStatus.GetLength(1); y++)
                {
                    fieldsStatus[i, y].AttackedWhiteFields = newFieldsStatusProperty[i, y];
                }
            }
        }
        private static void UpdateBlackAttackFielsStatus(FieldsStatus[,] fieldsStatus, bool[,] newFieldsStatusProperty)
        {
            for (int i = 0; i < fieldsStatus.GetLength(0); i++)
            {
                for (int y = 0; y < fieldsStatus.GetLength(1); y++)
                {
                    fieldsStatus[i, y].AttackedBlackFields = newFieldsStatusProperty[i, y];
                }
            }
        }
        private static void UpdateFieldsStatus(Figure figure, FieldsStatus[,] fieldsStatus, Vector2 newVector2)
        {
            if(figure.Color == FigureColor.Black)
            {
                fieldsStatus[figure.Vector2.X, figure.Vector2.Y].OccupiedBlackFields = false;
                fieldsStatus[newVector2.X, newVector2.Y].OccupiedBlackFields = true;
            }
            else
            {
                fieldsStatus[figure.Vector2.X, figure.Vector2.Y].OccupiedWhiteFields = false;
                fieldsStatus[newVector2.X, newVector2.Y].OccupiedWhiteFields = true;
            }
            fieldsStatus[figure.Vector2.X, figure.Vector2.Y].Figure = null;
            fieldsStatus[newVector2.X, newVector2.Y].Figure = figure;
        }
    }
}
