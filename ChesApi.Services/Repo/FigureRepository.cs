using Chess.Core.Domain;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Repo
{
    public class FigureRepository : IFigureRepository
    {
        public Figure GetFigure(LiveGame liveGame, Guid figureId)
           => liveGame.Figures.FirstOrDefault(x => x.Id == figureId);

        public Figure GetFigure(LiveGame liveGame, int y, int x)
            => liveGame.Figures.FirstOrDefault(z => z.X == x && z.Y == y);
 
        public void RemoveFigure(LiveGame liveGame, Figure figure)
        {
            liveGame.Figures.Remove(figure);
        }
    }
}
