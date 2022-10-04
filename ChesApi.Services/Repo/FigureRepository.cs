using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
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
        public Figure? GetFigure(LiveGame liveGame, Vector2 vector2)
            => liveGame.FieldsStatus[vector2.X, vector2.Y].Figure;

        public Figure GetKing(LiveGame liveGmae, bool color)
            => liveGmae.Figures.First(x => x.FigureType == FigureType.King && x.WhiteColor == color);
 
        public void RemoveFigure(LiveGame liveGame, Figure figure)
            => liveGame.Figures.Remove(figure);

        public IEnumerable<Figure> GetFiguresIsAttacking(LiveGame liveGame, bool figureColor)
            => liveGame.Figures.Where(x => x.IsAttacking == true && x.WhiteColor == figureColor);

        public IEnumerable<Figure> GetFiguresByColor(LiveGame liveGame, bool figureColor)
            => liveGame.Figures.Where(x => x.WhiteColor == figureColor);
    }
}
