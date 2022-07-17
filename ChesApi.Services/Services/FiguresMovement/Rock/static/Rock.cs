using ChesApi.Services.Services.EnumDirection;
using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.FiguresMovement.Rock.@static
{
    public static class Rock
    {
        public static EnumRockDirection RockDirection(int oldX, int oldY, int x, int y)
        {
            if (oldX < x && oldY == y)
            {
                return EnumRockDirection.down;
            }
            if (oldX > x && oldY == y)
            {
                return EnumRockDirection.up;
            }
            if (oldY < y && oldX == x)
            {
                return EnumRockDirection.right;
            }
            if (oldY > y && oldX == x)
            {
                return EnumRockDirection.left;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
