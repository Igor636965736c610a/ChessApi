using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class PownMoveStrategy : IFigureTypeMoveStrategy
    {
        public void Move(Figure figure, LiveGame liveGame, Vector2 newVector2, EnumDirection enumDirection, IEnumerable<Figure> enemyFigures)
        {
            throw new NotImplementedException();
        }
    }
}
