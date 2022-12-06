using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Factory
{
    public class Factory
    {
        public static Player GetPlayer(Guid userId, string name, string connectionId, bool hasMove, bool whiteColor)
            => new Player(userId, name, connectionId,  hasMove, whiteColor);
        public static LiveGame GetGame(Player player1, Player player2)
        {
            var board = GetStandardBoard();
            return new LiveGame(board, player1, player2);
        }
        private static Board GetStandardBoard()
        {
            var figures = new List<Figure>()
            {
                new Rook(Value.five, false, new Vector2(0, 0)),
                new Rook( Value.five,false, new Vector2(7, 0)),
                new Knight(Value.three, false, new Vector2(1, 0)),
                new Knight(Value.three, false, new Vector2(6, 0)),
                new Bishop(Value.three, false, new Vector2(2, 0)),
                new Bishop(Value.three, false, new Vector2(5, 0)),
                new Queen(Value.eight, false, new Vector2(3, 0)),
                new King(Value.none, false, new Vector2(4, 0)),
                new Pown(Value.one, false, new Vector2(0, 1)),
                new Pown(Value.one, false, new Vector2(1, 1)),
                new Pown(Value.one, false, new Vector2(2, 1)),
                new Pown(Value.one, false, new Vector2(3, 1)),
                new Pown(Value.one, false, new Vector2(4, 1)),
                new Pown(Value.one, false, new Vector2(5, 1)),
                new Pown(Value.one, false, new Vector2(6, 1)),
                new Pown(Value.one, false, new Vector2(7, 1)),
                new Rook(Value.five, true, new Vector2(0, 7)),
                new Rook(Value.five, true, new Vector2(7, 7)),
                new Knight(Value.three, true, new Vector2(1, 7)),
                new Knight(Value.three, true, new Vector2(6, 7)),
                new Bishop(Value.three, true, new Vector2(2, 7)),
                new Bishop(Value.three, true, new Vector2(5, 7)),
                new Queen(Value.eight, true, new Vector2(3, 7)),
                new King(Value.none, true, new Vector2(4, 7)),
                new Pown(Value.one, true, new Vector2(0, 6)),
                new Pown(Value.one, true, new Vector2(1, 6)),
                new Pown(Value.one, true, new Vector2(2, 6)),
                new Pown(Value.one, true, new Vector2(3, 6)),
                new Pown(Value.one, true, new Vector2(4, 6)),
                new Pown(Value.one, true, new Vector2(5, 6)),
                new Pown(Value.one, true, new Vector2(6, 6)),
                new Pown(Value.one, true, new Vector2(7, 6))
            };
            Vector2 maxDimensions = new(8, 8);
            Figure?[,] fieldsStatus = new Figure?[maxDimensions.X, maxDimensions.Y];
            foreach(var f in figures)
            {
                fieldsStatus[f.Vector2.X, f.Vector2.Y] = f;
            }

            return new Board(figures, fieldsStatus, maxDimensions);
        }
    }
}
