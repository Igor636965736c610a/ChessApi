using Chess.Core.Domain.Enums;
using Chess.Core.Domain.DefaultConst;
using Chess.Core.Repo.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Core.Repo.Game;
using ChesApi.Infrastructure.Services.AttackedFiels;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain;
using System.Collections;
using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.static_methods;

namespace ChesApi.Infrastructure.Services
{
    public class MoveService
    {
        private readonly IUserInGameRepository _userInGameRepository;
        private readonly IFigureRepository _figureRepository;
        private readonly IFigureTypeMoveStrategySelector _figureTypeMoveStrategySelector;
        public MoveService
            (IUserInGameRepository userInGameRepository, IFigureRepository figureRepository,
            FigureTypeMoveStrategySelector figureTypeMoveStrategySelector, ISetNewAttackFields setNewAttackFields)
        {
            _userInGameRepository = userInGameRepository;
            _figureRepository = figureRepository;
            _figureTypeMoveStrategySelector = figureTypeMoveStrategySelector;
        }

        public GameStatus Move(Vector2 newVector2, Vector2 oldVector2, Guid userId)  //userId from JWT
        {
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

            if (user.FigureColor != liveGame.FigureColour)
                throw new InvalidOperationException();

            if (oldVector2.X == newVector2.X && oldVector2.Y == newVector2.Y)
                throw new InvalidOperationException();

            if(!figure.CheckLegalMoveDirection(newVector2))
                throw new InvalidOperationException();
            var direction = figure.SetDirection(newVector2);

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
            var figureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(figure, _figureRepository);
            figureMoveStrategy.Move(figure, liveGame, newVector2, direction, enemyFigures);
            Figure? toRemoveFigure = enemyFigures.FirstOrDefault(x => x.Vector2.X == newVector2.X && x.Vector2.Y == newVector2.Y);
            if (toRemoveFigure is not null)
            {
                if(figure.Color == FigureColor.White)
                    liveGame.FieldsStatus[newVector2.X, newVector2.Y].OccupiedWhiteFields = false;
                else
                    liveGame.FieldsStatus[newVector2.X, newVector2.Y].OccupiedBlackFields = false;
                _figureRepository.RemoveFigure(liveGame, toRemoveFigure);
            }
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
                if (liveGame.FieldsStatus[blackKing.X, blackKing.Y].AttackedWhiteFields && CheckCheckmate(liveGame, FigureColor.White, blackKing))
                    gameStatus = GameStatus.WhiteMat;

                liveGame.FigureColour = FigureColor.Black;
                user.FigureColor = FigureColor.Black;
            }
            if (figure.Color == FigureColor.Black)
            {
                if (liveGame.FieldsStatus[whiteKing.X, whiteKing.Y].AttackedBlackFields && CheckCheckmate(liveGame, FigureColor.Black, whiteKing))
                    gameStatus = GameStatus.BlackMat;

                liveGame.FigureColour = FigureColor.White;
                user.FigureColor = FigureColor.White;
            }
            foreach(var f in liveGame.Figures)
            {
                f.IsAttacking = false;
            }
            return gameStatus;
        }

        private bool CheckCheckmate(LiveGame liveGame, FigureColor figureColor, Figure enemyKing, Figure king)
        {
            //sprawdzenie legalności ruchow krola


            var attackingFigures = _figureRepository.GetFiguresIsAttacking(liveGame, figureColor);
            if(attackingFigures.Count() > 1)
            {
                if(attackingFigures.Any(x => x.FigureType == FigureType.Knight))
                    return true;

                if(attackingFigures.Any(x => x.FigureType == FigureType.Rook) && attackingFigures
                    .Any(x => x.FigureType == FigureType.Bishop))
                    return true;

                List<EnumDirection> attackDirections = new();
                foreach(var f in attackingFigures)
                {
                    attackDirections.Add(f.SetDirection(enemyKing.Vector2));
                }
                if(!attackDirections.All(x => x == attackDirections.First()))
                    return true;

            }
            var defendingFigures = _figureRepository.GetFiguresByColor(liveGame, enemyKing.Color)
                .SkipWhile(x => x.FigureType == FigureType.King);
            var figure = attackingFigures.OrderBy(x => Math.Abs(enemyKing.Vector2.X + enemyKing.Vector2.Y - x.Vector2.X + x.Vector2.Y)).First();
            var direction = figure.SetDirection(enemyKing.Vector2);
            return !figure.CheckCheckamte(enemyKing.Vector2, defendingFigures, attackingFigures, liveGame.FieldsStatus,
                king.Vector2, direction);
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
