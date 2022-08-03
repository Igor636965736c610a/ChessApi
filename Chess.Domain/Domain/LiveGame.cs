using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
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
        public FielsStatus[,] FielsStatus { get; set; }

        //public bool[,] AttackedWhiteFiels { get; set; }
        //public bool[,] AttackedBlackFiels { get; set; }
        //public bool[,] OccupiedWhiteFiels { get; set; }
        //public bool[,] OccupiedBlackFiels { get; set; }

        public Guid Id { get; set; }
        public User HostUser { get; set; }
        public User User2 { get; set; }
        public FigureColour FigureColour { get; set; } = FigureColour.White;
        public LiveGame(List<Figure> figures, FielsStatus[,] fielsStatus, User userHost)
        {
            Figures = figures;
            FielsStatus = fielsStatus;
            HostUser = userHost;
        }
    }
}
