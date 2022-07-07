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
                if (liveGame.OccupiedBlackFieles[i, x])
                {
                    throw new InvalidOperationException();
                }               
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFieles[y, x] = true;
                        liveGame.OccupiedWhiteFieles[oldY, x] = false;
                        break;
                    }
            
                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFieles[y, x] = true;
                        liveGame.OccupiedBlackFieles[oldY, x] = false;
                        break;
                    }
            }
        }
        public void RockDownMovement(int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldY + 1; i < y; i++)
            {
                if (liveGame.OccupiedBlackFieles[i, x])
                {
                    throw new InvalidOperationException();
                }
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFieles[y, x] = true;
                        liveGame.OccupiedWhiteFieles[oldY, x] = false;
                        break;
                    }

                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFieles[y, x] = true;
                        liveGame.OccupiedBlackFieles[oldY, x] = false;
                        break;
                    }
            }
        }
        public void RockRightMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX + 1; i < x; i++)
            {
                if (liveGame.OccupiedBlackFieles[i, y])
                {
                    throw new InvalidOperationException();
                }
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFieles[y, x] = true;
                        liveGame.OccupiedWhiteFieles[y, oldX] = false;
                        break;
                    }

                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFieles[y, x] = true;
                        liveGame.OccupiedBlackFieles[y, oldX] = false;
                        break;
                    }
            }
        }
        public void RockLeftMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX - 1; i > x; i--)
            {
                if (liveGame.OccupiedBlackFieles[i, y])
                {
                    throw new InvalidOperationException();
                }
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedWhiteFieles[y, x] = true;
                        liveGame.OccupiedWhiteFieles[y, oldX] = false;
                        break;
                    }

                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFieles[y, x])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFieles[y, x])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, y, x);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        liveGame.OccupiedBlackFieles[y, x] = true;
                        liveGame.OccupiedBlackFieles[y, oldX] = false;
                        break;
                    }
            }
        }
    }
}
