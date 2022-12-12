﻿using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public class Pown : Figure
    {
        private bool FirstMove { get; set; } = true;
        public Pown(Value value, bool color, Vector2 vector2) : base(value, color, vector2)
        {
            FigureType = FigureType.Pown;
        }

        //public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        //{
        //    throw new NotImplementedException();
        //}
        public override bool ChcekLegalMovement(Board board, Vector2 newVector2, List<Figure> enemyFigures)
        {
            var fieldsStatus = board.FieldsStatus;
            var king = UtilsMethods.GetKing(board.Figures, WhiteColor);
            Vector2 movement;
            Vector2 direction;
            if(!CheckDirectionValid(newVector2, out movement, out direction))
                return false;
            if (!UtilsMethods.CheckRevealAttack(Vector2, king.Vector2, board, enemyFigures))
                return false;
            if (Math.Abs(movement.Y) == 2 && FirstMove == false)
                return false;
            if(Math.Abs(direction.X) == 1 && ((fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor == WhiteColor)
                || (board.EnPassant.CanEnPassant == true 
                && board.EnPassant.Vector2.X == newVector2.X 
                && board.EnPassant.Vector2.Y == newVector2.Y)))
                return false; 
            if(Math.Abs(movement.Y) == 2 && !UtilsMethods.LegalMovement(fieldsStatus, Vector2, newVector2, direction, WhiteColor))
                return false;
            return !(direction.X == 0 && fieldsStatus[newVector2.X, newVector2.Y]?.WhiteColor != WhiteColor);
        }
        public override void SetNewPosition(Vector2 newVector2, Board board)
        {
            Figure? figureToDelete;
            if (board.EnPassant.CanEnPassant && newVector2.X == board.EnPassant.Vector2.X && newVector2.Y == board.EnPassant.Vector2.Y)
                figureToDelete = board.FieldsStatus[newVector2.X, Vector2.Y];
            else
                figureToDelete = board.FieldsStatus[newVector2.X, newVector2.Y];
            if (figureToDelete is not null)
                board.Figures.Remove(figureToDelete);
            if (Math.Abs(newVector2.Y - Vector2.Y) == 2)
                board.EnPassant = new EnPassant(true, new Vector2(newVector2.X, newVector2.Y - Math.Sign(newVector2.Y - Vector2.Y)));
            else
                board.EnPassant = new EnPassant();
            board.FieldsStatus[Vector2.X, Vector2.Y] = null;
            board.FieldsStatus[newVector2.X, newVector2.Y] = this;
            Vector2 = newVector2;
        }
        private bool CheckDirectionValid(Vector2 newVector2, out Vector2 movement, out Vector2 dir)
        {
            movement = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);
            dir = new Vector2(Math.Sign(movement.X), Math.Sign(movement.Y));
            if(dir.Y == 0)
                return false;
            if (Math.Abs(movement.Y) > 2 || Math.Abs(movement.X) > 1)
                return false;
            if (Math.Abs(movement.Y) == 2 && Math.Abs(dir.X) > 0)
                return false;
            return WhiteColor ? dir.Y > 0 : dir.Y < 0;
        }

        public override bool[,] ShowLegalMovement(Board board, List<Figure> attackingFigures)
        {
            throw new NotImplementedException();
        }

        public override Vector2[] GetDirs()
        {
            throw new NotImplementedException();
        }
    }
}
