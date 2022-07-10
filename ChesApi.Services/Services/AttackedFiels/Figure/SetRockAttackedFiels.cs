using Chess.Core.Domain.DefaultConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.AttackedFiels.Figure
{
    public static class SetRockAttackedFiels
    {
        public static void RockAttakedFiels(bool[,] ocupiedBlackFiels, bool[,] ocupiedWhiteFiels, bool[,] newAttackedFiels, int y, int x)
        {
            Up(ocupiedBlackFiels, ocupiedWhiteFiels, newAttackedFiels, y, x);
            Down(ocupiedBlackFiels, ocupiedWhiteFiels, newAttackedFiels, y, x);
            Left(ocupiedBlackFiels, ocupiedWhiteFiels, newAttackedFiels, y, x);
            Right(ocupiedBlackFiels, ocupiedWhiteFiels, newAttackedFiels, y, x);
        }
        private static void Up(bool[,] ocupiedBlackFiels, bool[,] ocupiedWhiteFiels, bool[,] newAttackedFiels, int y, int x)
        {
            for(int i = y - 1; i >= 0; i--)
            {
                newAttackedFiels[i, x] = true;
                if(ocupiedBlackFiels[i, x] || ocupiedWhiteFiels[i, x])
                {
                    break;
                }
            }
        }
        private static void Down(bool[,] ocupiedBlackFiels, bool[,] ocupiedWhiteFiels, bool[,] newAttackedFiels, int y, int x)
        {
            for (int i = y + 1; i <= 7; i++)
            {
                newAttackedFiels[i, x] = true;
                if (ocupiedBlackFiels[i, x] || ocupiedWhiteFiels[i, x])
                {
                    break;
                }
            }
        }
        private static void Left(bool[,] ocupiedBlackFiels, bool[,] ocupiedWhiteFiels, bool[,] newAttackedFiels, int y, int x)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                newAttackedFiels[y, i] = true;
                if (ocupiedBlackFiels[y, i] || ocupiedWhiteFiels[i, x])
                {
                    break;
                }
            }
        }
        private static void Right(bool[,] ocupiedBlackFiels, bool[,] ocupiedWhiteFiels, bool[,] newAttackedFiels, int y, int x)
        {
            for (int i = x + 1; i <= 7; i++)
            {
                newAttackedFiels[y, i] = true;
                if (ocupiedBlackFiels[y, i] || ocupiedWhiteFiels[i, x])
                {
                    break;
                }
            }
        }
    }
}
