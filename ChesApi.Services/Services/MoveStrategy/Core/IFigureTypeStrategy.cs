using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.MoveStrategy.MoveDirectionStrategy.Core
{
    public interface IFigureTypeMoveStrategy
    {
        void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY);
    }
}
