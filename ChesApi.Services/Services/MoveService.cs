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

namespace ChesApi.Infrastructure.Services
{
    public class MoveService
    {
        private readonly IUserInGameRepository _userInGameRepository;
        private readonly IFigureRepository _figureRepository;
        private readonly IFigureTypeMoveStrategySelector _figureTypeMoveStrategySelector;
        private readonly ISetNewAttackFieles _setNewAttackFieles;
        public MoveService
            (IUserInGameRepository userInGameRepository, IFigureRepository figureRepository,
            FigureTypeMoveStrategySelector figureTypeMoveStrategySelector, ISetNewAttackFieles setNewAttackFieles)
        {
            _userInGameRepository = userInGameRepository;
            _figureRepository = figureRepository;
            _figureTypeMoveStrategySelector = figureTypeMoveStrategySelector;
            _setNewAttackFieles = setNewAttackFieles;
        }

        public GameStatus Move(int x, int y, Guid userId, Guid figureId)  //userId from JWT
        {
            GameStatus gameStatus = GameStatus.IsGaming;
            if (x <= Board.X || x > 1 || y <= Board.Y || y > 1)
            {
                throw new Exception("X and Y must be bigger than 0 and lower than 9");
            }
            var user = _userInGameRepository.GetUserById(userId);
            if (user is null)
            {
                throw new NullReferenceException();
            }
            var liveGame = user.LiveGame;
            if(liveGame is null)
            {
                throw new Exception("404 sesion");
            }
            if(liveGame.IsGaming == false)
            {
                throw new InvalidOperationException();
            }
            var figure = _figureRepository.GetFigure(liveGame, figureId);
            if(figure is null)
            {
                throw new NullReferenceException();
            }
            if (user.FigureColor != liveGame.FigureColour)
            {
                throw new InvalidOperationException();
            }
            int oldX = figure.X;
            int oldY = figure.Y;
            int newX = x - 1;
            int newY = y - 1;
            if (oldX == newX && oldY == newY)
            {
                throw new InvalidOperationException();
            }

            var figureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(figure, _figureRepository, _setNewAttackFieles);
            var direction = figureMoveStrategy.SetDirection(oldX, oldY, newX, newY);
            figureMoveStrategy.Move(figure, liveGame, oldX, oldY, newX, newY, direction);
            var whiteKing = _figureRepository.GetKing(liveGame, FigureColor.White);
            var blackKing = _figureRepository.GetKing(liveGame, FigureColor.Black);
            bool[,] newWhiteAttackFields = _setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColor.White, whiteKing);
            bool[,] newBlackAttackFields = _setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColor.Black, blackKing);
            UpdateWhiteAttackFieldsStatus(liveGame.FieldsStatus, newWhiteAttackFields);
            UpdateBlackAttackFielsStatus(liveGame.FieldsStatus, newBlackAttackFields);

            if (figure.Color == FigureColor.White)
            {
                if (liveGame.FieldsStatus[blackKing.X, blackKing.Y].AttackedWhiteFields && CheckCheckmate(liveGame, FigureColor.White, blackKing))
                {
                    gameStatus = GameStatus.WhiteMat;
                }
                liveGame.FigureColour = FigureColor.Black;
                user.FigureColor = FigureColor.Black;
            }
            if (figure.Color == FigureColor.Black)
            {
                if (liveGame.FieldsStatus[whiteKing.X, whiteKing.Y].AttackedBlackFields && CheckCheckmate(liveGame, FigureColor.Black, whiteKing))
                {
                    gameStatus = GameStatus.BlackMat;
                }
                liveGame.FigureColour = FigureColor.White;
                user.FigureColor = FigureColor.White;
            }
            foreach(var f in liveGame.Figures)
            {
                f.IsAttacking = false;
            }
            return gameStatus;
        }

        private bool CheckCheckmate(LiveGame liveGame, FigureColor figureColor, Figure king)
        {
            //sprawdzenie legalności ruchow krola


            var attackingFigures = _figureRepository.GetFiguresIsAttacking(liveGame, figureColor);
            if(attackingFigures.Count() > 1)
            {
                if(attackingFigures.Any(x => x.FigureType == FigureType.Knight))
                {
                    return true;
                }
                if(attackingFigures.Any(x => x.FigureType == FigureType.Rock) && attackingFigures
                    .Any(x => x.FigureType == FigureType.Bishop))
                {
                    return true;
                }
                List<EnumDirection> attackDirections = new();
                foreach(var f in attackingFigures)
                {
                    var localFigureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(f, null, null);
                    attackDirections.Add(localFigureMoveStrategy.SetDirection(f.X, f.Y, king.X, king.Y));
                }
                if(!attackDirections.All(x => x == attackDirections.First()))
                {
                    return true;
                }
            }
            var defendingFigures = _figureRepository.GetFiguresByColor(liveGame, king.Color)
                .SkipWhile(x => x.FigureType == FigureType.King);
            var figure = attackingFigures.OrderBy(x => Math.Abs(king.X + king.Y - x.X + x.Y)).First();
            var figureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(figure, null, null);
            var direction = figureMoveStrategy.SetDirection(king.X, king.Y, figure.X, figure.Y);
            return !figureMoveStrategy.CheckCheckMate
                (figure.X, figure.Y, king, defendingFigures, liveGame, direction, _figureTypeMoveStrategySelector);
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
    }
}
