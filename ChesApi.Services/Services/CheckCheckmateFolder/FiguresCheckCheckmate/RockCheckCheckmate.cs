using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.FiguresMovement.Rock.@static;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.CheckCheckmateFolder.FiguresCheckCheckmate
{
    public static class RockCheckCheckmate
    {
        public static bool Check(int x, int y, IEnumerable<Figure> defendingFigures, FielsStatus[,] fielsStatus)
        {
            foreach (var figure in defendingFigures)
            {
                int figureX = figure.X;
                int figureY = figure.Y;
                switch (figure.FigureType)
                {
                    case FigureType.Queen:
                        {
                            break;
                        }
                    case FigureType.Knight:
                        {
                            break;
                        }
                    case FigureType.Pown:
                        {
                            break;
                        }
                    case FigureType.King:
                        {
                            break;
                        }
                    case FigureType.Bishop:
                        {
                            break;
                        }
                    case FigureType.Rock:
                        {
                            if(figure.X != x && figure.Y != y)
                            {
                                break;
                            }
                            var direction = Rock.RockDirection(figureX, figureY, x, y);
                            switch(direction)
                            {
                                case EnumDirection.Up:
                                    {
                                        CoveringMoveFigures.RockUp();
                                        break;
                                    }
                                case EnumDirection.Down:
                                    {
                                        break;
                                    }
                                case EnumDirection.Left:
                                    {
                                        break;
                                    }
                                case EnumDirection.Right:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }
    }
}
