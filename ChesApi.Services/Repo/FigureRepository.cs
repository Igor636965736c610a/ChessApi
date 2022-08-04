using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Repo
{
    public class FigureRepository : IFigureRepository
    {
        public Figure GetFigure(LiveGame liveGame, Guid figureId)
           => liveGame.Figures.FirstOrDefault(x => x.Id == figureId);

        public Figure GetFigure(LiveGame liveGame, int y, int x)
            => liveGame.Figures.FirstOrDefault(z => z.X == x && z.Y == y);

        public Figure GetKing(LiveGame liveGmae, FigureColor color)
            => liveGmae.Figures.First(x => x.FigureType == FigureType.King && x.Color == color);
 
        public void RemoveFigure(LiveGame liveGame, Figure figure)
        {
            liveGame.Figures.Remove(figure);
        }
        public IEnumerable<Figure> GetFiguresIsAttacking(LiveGame liveGame, FigureColor figureColor)
            => liveGame.Figures.Where(x => x.IsAttacking == true && x.Color == figureColor);

        public IEnumerable<Figure> GetFiguresByColor(LiveGame liveGame, FigureColor figureColor)
            => liveGame.Figures.Where(x => x.Color == figureColor);
    }
}
