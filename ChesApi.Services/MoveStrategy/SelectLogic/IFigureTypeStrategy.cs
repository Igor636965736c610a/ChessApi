using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic
{
    public interface IFigureTypeMoveStrategy
    {
        void Move(Figure figure, LiveGame liveGame, Vector2 newVector2, EnumDirection enumDirection,
            IEnumerable<Figure> enemyFigures);
    }
}
