using ChesApi.Services.Services.EnumDirection.Rock;
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
        public static RockEnumDirection RockDirection(int oldX, int oldY, int x, int y)
        {
            if (oldX < x && oldY == y)
            {
                return RockEnumDirection.down;
            }
            if (oldX > x && oldY == y)
            {
                return RockEnumDirection.up;
            }
            if (oldY < y && oldX == x)
            {
                return RockEnumDirection.right;
            }
            if (oldY > y && oldX == x)
            {
                return RockEnumDirection.left;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
