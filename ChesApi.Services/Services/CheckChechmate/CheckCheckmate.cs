using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services.CheckChechmate
{
    public static class CheckCheckmate
    {
        public static bool RockUp(int x, int y, int kingY, IEnumerable<Figure> defendingFigures)
        {
            for (int i = y ; i > kingY; i--)
            {
                
            }
        }
        public static bool RockDown(int x, int y, int kingY, IEnumerable<Figure> defendingFigures)
        {
            for (int i = y; i < kingY; i++)
            {

            }
        }
        public static bool RockLeft(int x, int y, int kingX, IEnumerable<Figure> defendingFigures)
        {
            for (int i = x; i > kingX; i--)
            {

            }
        }
        public static bool RockRight(int x, int y, int kingX, IEnumerable<Figure> defendingFigures)
        {
            for (int i = x; i < kingX; i++)
            {

            }
        }

    }
}
