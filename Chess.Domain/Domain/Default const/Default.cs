using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Default_const
{
    public class Default
    {
        public static List<int[,]> DefaultWhiteAttackedFieles { get; set; } = new List<int[,]>()
        {

        };
        public static List<int[,]> DefaultBlackAttackedFieles { get; set; } = new List<int[,]>()
        {

        };
        public static List<int[,]> DefaultWhiteOccupiedFieles { get; set; } = new List<int[,]>()
        {

        };
        public static List<int[,]> DefaultBlackOccupiedFieles { get; set; } = new List<int[,]>()
        {

        };
        public static List<Figure> Figures = new List<Figure>()
        {
            new Figure(new int[0, 0], FigureType.Rock, Value.five, FigureColour.black),
            new Figure(new int[0, 7], FigureType.Rock, Value.five, FigureColour.black),
            new Figure(new int[0, 1], FigureType.Knight, Value.three, FigureColour.black),
            new Figure(new int[0, 6], FigureType.Knight, Value.three, FigureColour.black),
            new Figure(new int[0, 2], FigureType.Bishop, Value.three, FigureColour.black),
            new Figure(new int[0, 5], FigureType.Bishop, Value.three, FigureColour.black),
            new Figure(new int[0, 3], FigureType.Queen, Value.eight, FigureColour.black),
            new Figure(new int[0, 4], FigureType.King, Value.none, FigureColour.black),
            new Figure(new int[7, 0], FigureType.Rock, Value.five, FigureColour.white),
            new Figure(new int[7, 7], FigureType.Rock, Value.five, FigureColour.white),
            new Figure(new int[7, 1], FigureType.Knight, Value.three, FigureColour.white),
            new Figure(new int[7, 6], FigureType.Knight, Value.three, FigureColour.white),
            new Figure(new int[7, 2], FigureType.Bishop, Value.three, FigureColour.white),
            new Figure(new int[7, 5], FigureType.Bishop, Value.three, FigureColour.white),
            new Figure(new int[7, 3], FigureType.Queen, Value.eight, FigureColour.white),
            new Figure(new int[7, 4], FigureType.King, Value.none, FigureColour.white),
            new Figure(new int[1, 0], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 1], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 2], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 3], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 4], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 5], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 6], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[1, 7], FigureType.Pown, Value.one, FigureColour.black),
            new Figure(new int[6, 0], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 1], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 2], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 3], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 4], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 5], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 6], FigureType.Pown, Value.one, FigureColour.white),
            new Figure(new int[6, 7], FigureType.Pown, Value.one, FigureColour.white),
        };
    }
}
