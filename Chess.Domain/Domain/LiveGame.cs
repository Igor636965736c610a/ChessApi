using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class LiveGame
    {
        public Board Board { get; set; }
        public bool IsGaming { get; set; } = false;
        public Guid Id { get; set; }
        public User HostUser { get; set; }
        public User? User2 { get; set; }
        public bool WhiteColor { get; set; } = true;
        public LiveGame(User userHost, Board board)
        {
            HostUser = userHost;
            Board = board;
        }
    }
}
