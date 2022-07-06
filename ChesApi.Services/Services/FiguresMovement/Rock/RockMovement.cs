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
    public class RockMovement
    {
        private readonly IFigureRepository _figureRepository;
        public RockMovement(IFigureRepository figureRepository)
        {
            _figureRepository = figureRepository;
        }

        public void RockUpMovement(int oldX, int x, int y, Figure figure, LiveGame liveGame)
        {
            for (int i = oldX - 1; i <= x - 1; i++)
            {
                switch (figure.Colour)
                {
                    case FigureColour.white:
                        {
                            if (liveGame.OccupiedBlackFieles[i, y] = true)
                            {
                                var toDeleteFigure = _figureRepository.GetFigure(liveGame, i, y);
                                _figureRepository.RemoveFigure(liveGame, toDeleteFigure);
                                figure.X = i;
                            }
                            break;
                        }
                    case FigureColour.black:
                        {
                            
                            break;
                        }

                }
            }
        }
        public static void RockDownMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {

        }
        public static void RockRightMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {

        }
        public static void RockLeftMovement(int oldX, int oldY, int x, int y, Figure figure, LiveGame liveGame)
        {

        }
    }
}
