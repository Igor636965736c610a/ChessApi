using ChesApi.Services.Services.FiguresMovement.Rock.@static;
using ChesApi.Services.Services.MoveStrategy.MoveDirectionStrategy.Core;
using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.MoveStrategy
{
    public class RockMoveStrategy : IFigureTypeMoveStrategy
    {
        public void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY)
        {
            var direction = Rock.RockDirection(oldX, oldY, newX, newY);
        }
    }
}
