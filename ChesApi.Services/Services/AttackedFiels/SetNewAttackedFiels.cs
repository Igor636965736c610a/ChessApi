using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChesApi.Services.Services.AttackedFiels.Figure;

namespace ChesApi.Services.Services.AttackedFiels
{
    public static class SetNewAttackedFiels
    {
        public static bool[,] SetNewAttackedBlackFiels(LiveGame liveGame, int oldY, int oldX)
        {
            bool[,] ocupiedWhiteFiels = liveGame.OccupiedWhiteFiels;
            bool[,] ocupiedBlackFiels = liveGame.OccupiedBlackFiels;
            var figures = liveGame.Figures.Where(x => x.Colour == FigureColour.black);
            ocupiedWhiteFiels[oldY, oldX] = false;
            bool[,] newAttackedBlackFiels = new bool[Board.Y, Board.X];
            foreach(var f in figures)
            {
                switch(f.FigureType)
                {
                    case FigureType.Queen:
                        {
                            break;
                        }
                    case FigureType.Pown:
                        {
                            break;
                        }
                    case FigureType.Bishop:
                        {
                            break;
                        }
                    case FigureType.Knight:
                        {
                            break;
                        }
                    case FigureType.Rock:
                        {
                            SetRockAttackedFiels.RockAttakedFiels(ocupiedBlackFiels, ocupiedWhiteFiels, newAttackedBlackFiels, oldY, oldX);
                            break;
                        }
                    case FigureType.King:
                        {
                            break;
                        }
                }
            }
            return newAttackedBlackFiels;
        }
        public static bool[,] SetNewAttackedWhiteFiels(LiveGame liveGame, int oldY, int oldX)
        {
            bool[,] ocupiedWhiteFiels = liveGame.OccupiedWhiteFiels;
            bool[,] ocupiedBlackFiels = liveGame.OccupiedBlackFiels;
            var figures = liveGame.Figures.Where(x => x.Colour == FigureColour.white);
            ocupiedBlackFiels[oldY, oldX] = false;
            bool[,] newAttackedWhiteFiels = new bool[Board.Y, Board.X];
            foreach (var f in figures)
            {
                switch (f.FigureType)
                {
                    case FigureType.Queen:
                        {
                            break;
                        }
                    case FigureType.Pown:
                        {
                            break;
                        }
                    case FigureType.Bishop:
                        {
                            break;
                        }
                    case FigureType.Knight:
                        {
                            break;
                        }
                    case FigureType.Rock:
                        {
                            SetRockAttackedFiels.RockAttakedFiels(ocupiedBlackFiels, ocupiedWhiteFiels, newAttackedWhiteFiels, oldY, oldX);
                            break;
                        }
                    case FigureType.King:
                        {
                            break;
                        }
                }
            }
            return newAttackedWhiteFiels;
        }
    }
}
