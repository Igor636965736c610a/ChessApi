using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.DefaultConst
{
    public class Default
    {
        public static FielsStatus[,] DefaultFielsStatus { get; set; } = new FielsStatus[Board.Y, Board.X]
        {
            
        };

        public static bool[,] DefaultWhiteAttackedFieles { get; set; } = new bool[Board.Y, Board.X]
        {
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true },
            { false, true, true, true, true, true, true, false },
        };
        public static bool[,] DefaultBlackAttackedFieles { get; set; } = new bool[Board.Y, Board.X]
        {
            { false, true, true, true, true, true, true, false },
            { true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
        };
        public static bool[,] DefaultWhiteOccupiedFieles { get; set; } = new bool[Board.Y, Board.X]
        {
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true },
        };
        public static bool[,] DefaultBlackOccupiedFieles { get; set; } = new bool[Board.Y, Board.X]
        {
            { true, true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true, true },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
        };
        public static List<Figure> Figures = new List<Figure>()
        {
            new Figure(0, 0, FigureType.Rock, Value.five, FigureColor.Black),
            new Figure(0, 7, FigureType.Rock, Value.five, FigureColor.Black),
            new Figure(0, 1, FigureType.Knight, Value.three, FigureColor.Black),
            new Figure(0, 6, FigureType.Knight, Value.three, FigureColor.Black),
            new Figure(0, 2, FigureType.Bishop, Value.three, FigureColor.Black),
            new Figure(0, 5, FigureType.Bishop, Value.three, FigureColor.Black),
            new Figure(0, 3, FigureType.Queen, Value.eight, FigureColor.Black),
            new Figure(0, 4, FigureType.King, Value.none, FigureColor.Black),
            new Figure(1, 0, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 1, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 2, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 3, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 4, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 5, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 6, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(1, 7, FigureType.Pown, Value.one, FigureColor.Black),
            new Figure(7, 0, FigureType.Rock, Value.five, FigureColor.White),
            new Figure(7, 7, FigureType.Rock, Value.five, FigureColor.White),
            new Figure(7, 1, FigureType.Knight, Value.three, FigureColor.White),
            new Figure(7, 6, FigureType.Knight, Value.three, FigureColor.White),
            new Figure(7, 2, FigureType.Bishop, Value.three, FigureColor.White),
            new Figure(7, 5, FigureType.Bishop, Value.three, FigureColor.White),
            new Figure(7, 3, FigureType.Queen, Value.eight, FigureColor.White),
            new Figure(7, 4, FigureType.King, Value.none, FigureColor.White),
            new Figure(6, 0, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 1, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 2, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 3, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 4, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 5, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 6, FigureType.Pown, Value.one, FigureColor.White),
            new Figure(6, 7, FigureType.Pown, Value.one, FigureColor.White),
        };
    }
}
