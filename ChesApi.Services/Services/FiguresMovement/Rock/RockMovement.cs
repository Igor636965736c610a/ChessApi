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

        public void RockUpMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX + 1; i <= x - 1; i++)
            {
                if (liveGame.OccupiedBlackFieles[i, y] || liveGame.OccupiedWhiteFieles[i,y] )
                {
                    throw new InvalidOperationException();
                }               
            }
            switch (figure.Colour)
            {
                case FigureColour.white:
                    {
                        if (liveGame.OccupiedWhiteFieles[x, y])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedBlackFieles[x, y])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, x, y);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        break;
                    }
            
                case FigureColour.black:
                    {
                        if (liveGame.OccupiedBlackFieles[x, y])
                        {
                            throw new InvalidOperationException();
                        }
                        if (liveGame.OccupiedWhiteFieles[x, y])
                        {
                            var toDeleteFigure = _figureRepository.GetFigure(liveGame, x, y);
                            _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                        }
                        figure.X = x;
                        break;
                    }
            }
        }
        public void RockDownMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {

        }
        public void RockRightMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {

        }
        public void RockLeftMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {

        }
    }
}
