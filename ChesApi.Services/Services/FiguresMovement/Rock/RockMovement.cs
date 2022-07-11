using AutoMapper;
using ChesApi.Services.Services.AttackedFiels;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.FiguresMovement.Rock
{
    public class RockMovement : IRockMovement
    {
        private readonly IFigureRepository _figureRepository;
        public RockMovement(IFigureRepository figureRepository)
        {
            _figureRepository = figureRepository;
        }

        public void RockUpMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY - 1; i > y; i--)
            {
                if (liveGame.OccupiedBlackFiels[i, x] || liveGame.OccupiedWhiteFiels[i, x])
                {
                    throw new InvalidOperationException();
                }               
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedBlackFiels = SetNewAttackedFiels.SetNewAttackedBlackFiels(liveGame, oldY, x);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedWhiteFiels[oldY, x] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFiels[y, x] = true;
                        break;
                    }
            
                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedWhiteFiels = SetNewAttackedFiels.SetNewAttackedWhiteFiels(liveGame, oldY, x);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedBlackFiels[oldY, x] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFiels[y, x] = true;
                        break;
                    }
            }
        }
        public void RockDownMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY + 1; i < y; i++)
            {
                if (liveGame.OccupiedBlackFiels[i, x] || liveGame.OccupiedWhiteFiels[i, x])
                {
                    throw new InvalidOperationException();
                }
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedBlackFiels = SetNewAttackedFiels.SetNewAttackedBlackFiels(liveGame, oldY, x);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedWhiteFiels[oldY, x] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFiels[y, x] = true;
                        break;
                    }

                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedWhiteFiels = SetNewAttackedFiels.SetNewAttackedWhiteFiels(liveGame, oldY, x);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedBlackFiels[oldY, x] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFiels[y, x] = true;
                        break;
                    }
            }
        }
        public void RockRightMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX + 1; i < x; i++)
            {
                if (liveGame.OccupiedBlackFiels[i, x] || liveGame.OccupiedWhiteFiels[i, x])
                {
                    throw new InvalidOperationException();
                }
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedBlackFiels = SetNewAttackedFiels.SetNewAttackedBlackFiels(liveGame, y, oldX);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedWhiteFiels[y, oldX] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFiels[y, x] = true;
                        break;
                    }

                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedWhiteFiels = SetNewAttackedFiels.SetNewAttackedWhiteFiels(liveGame, y, oldX);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedBlackFiels[y, oldX] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFiels[y, x] = true;
                        break;
                    }
            }
        }
        public void RockLeftMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX - 1; i > x; i--)
            {
                if (liveGame.OccupiedBlackFiels[i, x] || liveGame.OccupiedWhiteFiels[i, x])
                {
                    throw new InvalidOperationException();
                }
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedBlackFiels = SetNewAttackedFiels.SetNewAttackedBlackFiels(liveGame, y, oldX);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedWhiteFiels[y, oldX] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFiels[y, x] = true;
                        break;
                    }

                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFiels[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedWhiteFiels = SetNewAttackedFiels.SetNewAttackedWhiteFiels(liveGame, y, oldX);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.OccupiedBlackFiels[y, oldX] = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFiels[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFiels[y, x] = true;
                        break;
                    }
            }
        }
    }
}
