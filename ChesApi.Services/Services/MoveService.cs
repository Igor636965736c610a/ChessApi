using Chess.Core.Domain.Enums;
using Chess.Core.Domain.DefaultConst;
using Chess.Core.Repo.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Core.Repo.Game;
using ChesApi.Services.Services.FiguresMovement.Rock.@static;
using ChesApi.Services.Services.FiguresMovement.Rock;
using ChesApi.Services.Services.AttackedFiels;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain;
using System.Collections;
using ChesApi.Services.Services.CheckCheckmateFolder;
using ChesApi.Services.Services.EnumFiguresDirection;

namespace ChesApi.Services.Services
{
    public class MoveService
    {
        private readonly IUserInGameRepository _userInGameRepository;
        private readonly IFigureRepository _figureRepository;
        private readonly IRockMovement _rockMovement;
        public MoveService
            (IUserInGameRepository userInGameRepository, IFigureRepository figureRepository,
            IRockMovement rockMovement)
        {
            _userInGameRepository = userInGameRepository;
            _figureRepository = figureRepository;
            _rockMovement = rockMovement;
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
                        var direction = Rock.RockDirection(oldX, oldY, newX, newY);
                        switch(direction)
                        {
                            case EnumDirection.Up:
                                {
                                    _rockMovement.RockUpMovement(oldY, newX, newY, figure, liveGame);
                                    break;
                                }
                            case EnumDirection.Down:
                                {
                                    _rockMovement.RockDownMovement(oldY, newX, newY, figure, liveGame);
                                    break;
                                }
                            case EnumDirection.Left:
                                {
                                    _rockMovement.RockLeftMovement(oldX, newX, newY, figure, liveGame);
                                    break;
                                }
                            case EnumDirection.Right:
                                {
                                    _rockMovement.RockRightMovement(oldX, newX, newY, figure, liveGame);
                                    break;
                                }
                        }
                        break;
                    }
            }
            switch(figure.Colour)
            {
                case FigureColour.white:
                    {
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        SetNewAttackedFiels.SetNewAttackedWhiteFiels(liveGame, king);
                        if (liveGame.FielsStatus[king.Y, king.X].AttackedWhiteFiels)
                        {

                        }
                        else
                        {

                        }
                        break;
                    }
                case FigureColour.black:
                    {
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        SetNewAttackedFiels.SetNewAttackedBlackFiels(liveGame, king);
                        if (liveGame.FielsStatus[king.Y, king.X].AttackedBlackFiels)
                        {

                        }
                        else
                        {

                        }
                        break;
                    }
            }
            if (liveGame.FigureColour == FigureColour.white)
            {
                liveGame.FigureColour = FigureColour.black;
                user.FigureColour = FigureColour.black;
            }
            else
            {
                liveGame.FigureColour = FigureColour.white;
                user.FigureColour = FigureColour.white;
            }
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
                    switch (f.FigureType)
                    {
                        case FigureType.Queen:
                            {
                                break;
                            }
                        case FigureType.Pown:
                            {
                                break;
                            }
                        case FigureType.Bishop:
                            {
                                break;
                            }
                        case FigureType.Rock:
                            {
                                attackDirections.Add(Rock.RockDirection(f.X, f.Y, king.X, king.Y));
                                break;
                            }
                    }
                }
                if(!attackDirections.All(x => x == attackDirections.First()))
                {
                    return true;
                }
                attackingFigures.OrderBy(x => Math.Abs(king.X + king.Y - x.X + x.Y));
                // sprawdzenie najbliższej atakujacej figury w lini ataku
            }
            var color = king.Colour;
            var defendingFigures = _figureRepository.GetFiguresByColor(liveGame, color);
            FielsStatus[,] fielsStatus = liveGame.FielsStatus;
            var figures = attackingFigures.First();
            switch (figures.FigureType)
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
                        var direction = Rock.RockDirection(king.X, king.Y, figures.X, figures.Y);
                        switch (direction)
                        {
                            case EnumDirection.Up:
                                {
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
    }
}
