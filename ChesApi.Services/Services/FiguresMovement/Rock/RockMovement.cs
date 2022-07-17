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
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }               
            }
            SetNewPositoin(figure, liveGame, x, y, oldY, x);
        }
        public void RockDownMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY + 1; i < y; i++)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, oldY, x);
        }
        public void RockRightMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX + 1; i < x; i++)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, y, oldX);
        }
        public void RockLeftMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX - 1; i > x; i--)
            {
                if (liveGame.FielsStatus[i, x].OccupiedBlackFiels || liveGame.FielsStatus[i, x].OccupiedWhiteFiels)
                {
                    throw new InvalidOperationException();
                }
            }
            SetNewPositoin(figure, liveGame, x, y, y, oldX);   
        }

        private void SetNewPositoin(Figure figure, LiveGame liveGame, int x, int y, int oldY, int oldX)
        {
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedBlackFiels = SetNewAttackedFiels.SetNewAttackedBlackFiels(liveGame, oldY, oldX);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.white);
                        if (newAttackedBlackFiels[king.Y, king.X])
                        {
                            liveGame.FielsStatus[oldY, oldX].OccupiedWhiteFiels = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.FielsStatus[y, x].OccupiedWhiteFiels = true;
                        break;
                    }
                case FigureColour.black:
                    {
                        if (liveGame.FielsStatus[y, x].OccupiedBlackFiels)
                        {
                            throw new InvalidOperationException();
                        }
                        var newAttackedWhiteFiels = SetNewAttackedFiels.SetNewAttackedWhiteFiels(liveGame, oldY, oldX);
                        var king = _figureRepository.GetKing(liveGame, FigureColour.black);
                        if (newAttackedWhiteFiels[king.Y, king.X])
                        {
                            liveGame.FielsStatus[oldY, oldX].OccupiedBlackFiels = true;
                            throw new InvalidOperationException();
                        }
                        if (liveGame.FielsStatus[y, x].OccupiedWhiteFiels)
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.FielsStatus[y, x].OccupiedBlackFiels = true;
                        break;
                    }
            }
        }
    }
}
