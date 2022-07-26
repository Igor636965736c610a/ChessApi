using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.FiguresMovement.Rock.@static
{
    public static class Rock
    {
        public static EnumDirection RockDirection(int oldX, int oldY, int x, int y)
        {
            if (oldX < x && oldY == y)
            {
                return EnumDirection.Down;
            }
            if (oldX > x && oldY == y)
            {
                return EnumDirection.Up;
            }
            if (oldY < y && oldX == x)
            {
                return EnumDirection.Right;
            }
            if (oldY > y && oldX == x)
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
