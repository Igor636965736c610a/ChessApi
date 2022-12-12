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
        public Board Board { get; private set; }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public bool WhiteColor { get; private set; } = true;
        
        public LiveGame(Board board, Player player1, Player player2)
        {
            Board = board;
            Player1 = player1;
            Player2 = player2;
            player1.GameId = Id;
            player2.GameId = Id;
        }
    }
}
