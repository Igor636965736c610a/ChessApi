using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.FiguresMovement.Rock
{
    public static class Rock
    {
        public static void RockDirection(int oldX, int oldY, int x, int y, Figure figure, User user)
        {
            if (oldX < x && oldY == y)
            {
                
            }
            if (oldX > x && oldY == y)
            {
                
            }
            if (oldY < y && oldX == x)
            {
                
            }
            if (oldY > y && oldX == x)
            {
                
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
