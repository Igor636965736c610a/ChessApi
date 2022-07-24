using ChesApi.Services.Services.EnumFiguresDirection;
using ChesApi.Services.Services.FiguresMovement.Rock.@static;
using ChesApi.Services.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
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
        public void Move(Figure figure, LiveGame liveGame, int oldX, int oldY, int newX, int newY, EnumDirection enumDirection)
        {
            
        }
        public EnumDirection SetDirection(int oldX, int oldY, int newX, int newY)
        {
            if (oldX < newX && oldY == newY)
            {
                return EnumDirection.Down;
            }
            if (oldX > newX && oldY == newY)
            {
                return EnumDirection.Up;
            }
            if (oldY < newY && oldX == newX)
            {
                return EnumDirection.Right;
            }
            if (oldY > newY && oldX == newX)
            {
                return EnumDirection.Left;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
