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
        public bool StartGame { get; set; }= false;
        public bool IsGaming { get; set; } = false;
        public bool WhiteMat { get; set; } = false;
        public bool BlackMat { get; set; } = false;
        public bool[,] AttackedWhiteFiels { get; set; }
        public bool[,] AttackedBlackFiels { get; set; }
        public bool[,] OccupiedWhiteFieles { get; set; }
        public bool[,] OccupiedBlackFieles { get; set; }
        public Guid Id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
        public LiveGame(List<Figure> figures, bool[,] attackedWhiteFiels, bool[,] attackedBlackFiels, bool[,] occupiedWhiteFieles, bool[,] occupiedBlackFieles)
        {
            Figures = figures;
            AttackedWhiteFiels = attackedWhiteFiels;
            AttackedBlackFiels = attackedBlackFiels;
            OccupiedWhiteFieles = occupiedWhiteFieles;
            OccupiedBlackFieles = occupiedBlackFieles;
        }
    }
}
