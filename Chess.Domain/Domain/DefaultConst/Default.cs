using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.DefaultConst
{
    public class Default
    {
        public static bool[,] DefaultWhiteAttackedFieles { get; set; } = new bool[DefaultConst.Board.X,DefaultConst.Board.Y]
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
        public static bool[,] DefaultBlackAttackedFieles { get; set; } = new bool[DefaultConst.Board.X, DefaultConst.Board.Y]
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
        public static bool[,] DefaultWhiteOccupiedFieles { get; set; } = new bool[DefaultConst.Board.X, DefaultConst.Board.Y]
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
        public static bool[,] DefaultBlackOccupiedFieles { get; set; } = new bool[DefaultConst.Board.X, DefaultConst.Board.Y]
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
            new Figure(1, 1, FigureType.Rock, Value.five, FigureColour.black),
            new Figure(1, 8, FigureType.Rock, Value.five, FigureColour.black),
            new Figure(1, 2, FigureType.Knight, Value.three, FigureColour.black),
            new Figure(1, 7, FigureType.Knight, Value.three, FigureColour.black),
            new Figure(1, 3, FigureType.Bishop, Value.three, FigureColour.black),
            new Figure(1, 6, FigureType.Bishop, Value.three, FigureColour.black),
            new Figure(1, 4, FigureType.Queen, Value.eight, FigureColour.black),
            new Figure(1, 5, FigureType.King, Value.none, FigureColour.black),
            new Figure(2, 1, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 2, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 3, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 4, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 5, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 6, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 7, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(2, 8, FigureType.Pown, Value.one, FigureColour.black),
            new Figure(8, 1, FigureType.Rock, Value.five, FigureColour.white),
            new Figure(8, 8, FigureType.Rock, Value.five, FigureColour.white),
            new Figure(8, 2, FigureType.Knight, Value.three, FigureColour.white),
            new Figure(8, 7, FigureType.Knight, Value.three, FigureColour.white),
            new Figure(8, 3, FigureType.Bishop, Value.three, FigureColour.white),
            new Figure(8, 6, FigureType.Bishop, Value.three, FigureColour.white),
            new Figure(8, 4, FigureType.Queen, Value.eight, FigureColour.white),
            new Figure(8, 5, FigureType.King, Value.none, FigureColour.white),
            new Figure(7, 1, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 2, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 3, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 4, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 5, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 6, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 7, FigureType.Pown, Value.one, FigureColour.white),
            new Figure(7, 8, FigureType.Pown, Value.one, FigureColour.white),
        };
    }
}
