using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class LiveGame
    {
        public List<Figure> Figures = new List<Figure>();
        public bool IsGaming { get; set; } = false;
        public bool WhiteCheck { get; set; } = false;
        public bool BlackCheck { get; set; } = false;
        public bool WhiteMat { get; set; } = false;
        public bool BlackMat { get; set; } = false;
        public bool[,] AttackedWhiteFiels { get; set; }
        public bool[,] AttackedBlackFiels { get; set; }
        public bool[,] OccupiedWhiteFiels { get; set; }
        public bool[,] OccupiedBlackFiels { get; set; }
        public Guid Id { get; set; }
        public User HostUser { get; set; }
        public User User2 { get; set; }
        public FigureColour FigureColour { get; set; } = FigureColour.white;
        public LiveGame(List<Figure> figures, bool[,] attackedWhiteFiels, bool[,] attackedBlackFiels,
            bool[,] occupiedWhiteFieles, bool[,] occupiedBlackFieles, User userHost)
        {
            Figures = figures;
            AttackedWhiteFiels = attackedWhiteFiels;
            AttackedBlackFiels = attackedBlackFiels;
            OccupiedWhiteFiels = occupiedWhiteFieles;
            OccupiedBlackFiels = occupiedBlackFieles;
            HostUser = userHost;
        }
    }
}
