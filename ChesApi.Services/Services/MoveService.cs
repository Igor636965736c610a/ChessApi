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
using ChesApi.Infrastructure.Services.CheckCheckmateFolder;
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
            if (user.FigureColour != liveGame.FigureColour)
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
            var whiteKing = _figureRepository.GetKing(liveGame, FigureColour.White);
            var blackKing = _figureRepository.GetKing(liveGame, FigureColour.black);
            bool[,] newWhiteAttackFieles = _setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColour.White, whiteKing);
            bool[,] newBlackAttackFieles = _setNewAttackFieles.SetNewAttackFieles(liveGame, FigureColour.black, blackKing);
            UpdateWhiteAttackFielsStatus(liveGame.FielsStatus, newWhiteAttackFieles);
            UpdateBlackAttackFielsStatus(liveGame.FielsStatus, newBlackAttackFieles);

            if (figure.Colour == FigureColour.White)
            {
                if (liveGame.FielsStatus[blackKing.Y, blackKing.X].AttackedWhiteFiels)
                {
                    //check checkmate
                    //if chackmate => set gameStatus
                }
                liveGame.FigureColour = FigureColour.black;
                user.FigureColour = FigureColour.black;
            }
            if (figure.Colour == FigureColour.black)
            {
                if (liveGame.FielsStatus[whiteKing.Y, whiteKing.X].AttackedBlackFiels)
                {
                    //check checkmate
                    //if chackmate => set gameStatus
                }
                liveGame.FigureColour = FigureColour.White;
                user.FigureColour = FigureColour.White;
            }
            foreach(var f in liveGame.Figures)
            {
                f.IsAttacking = false;
            }
            return gameStatus;
        }

        private bool CheckCheckmate(LiveGame liveGame, FigureColour figureColor, Figure king)
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
                    var figureMoveStrategy = _figureTypeMoveStrategySelector.SelectMoveStrategy(f, null, null);
                    attackDirections.Add(figureMoveStrategy.SetDirection(f.X, f.Y, king.X, king.Y));
                }
                if(!attackDirections.All(x => x == attackDirections.First()))
                {
                    return true;
                }
            }
            var defendingFigures = _figureRepository.GetFiguresByColor(liveGame, king.Colour);
            var figure = attackingFigures.OrderBy(x => Math.Abs(king.X + king.Y - x.X + x.Y)).First();

            //strategia
            switch (figure.FigureType)
            {
                case FigureType.Queen:
                    {
                        break;
                    }
                case FigureType.Knight:
                    {
                        break;
                    }
                case FigureType.Pown:
                    {
                        break;
                    }
                case FigureType.King:
                    {
                        break;
                    }
                case FigureType.Bishop:
                    {
                        break;
                    }
                case FigureType.Rock:
                    {
                        var direction = Rock.RockDirection(king.X, king.Y, figure.X, figure.Y);
                        switch (direction)
                        {
                            case EnumDirection.Up:
                                {
                                    //to do strategii
                                    CheckCheckmateFigures.RockUp();
                                    break;
                                }
                            case EnumDirection.Down:
                                {

                                    break;
                                }
                            case EnumDirection.Left:
                                {

                                    break;
                                }
                            case EnumDirection.Right:
                                {

                                    break;
                                }
                        }
                        break;
                    }
            }
        }
        private void UpdateWhiteAttackFielsStatus(FielsStatus[,] fielsStatus, bool[,] newFielsStatusProperty)
        {
            for (int i = 0; i < fielsStatus.GetLength(0); i++)
            {
                for (int y = 0; y < fielsStatus.GetLength(1); y++)
                {
                    fielsStatus[i, y].AttackedWhiteFiels = newFielsStatusProperty[i, y];
                }
            }
        }
        private void UpdateBlackAttackFielsStatus(FielsStatus[,] fielsStatus, bool[,] newFielsStatusProperty)
        {
            for (int i = 0; i < fielsStatus.GetLength(0); i++)
            {
                for (int y = 0; y < fielsStatus.GetLength(1); y++)
                {
                    fielsStatus[i, y].AttackedBlackFiels = newFielsStatusProperty[i, y];
                }
            }
        }
    }
}
