using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Utils
{
    public static class UtilsMethods
    {
        public static bool LegalMovement(FieldsStatus[,] fieldsStatus, Vector2 current, Vector2 newVector2, Vector2 direction, 
            bool color)
        {
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));

            while ((current.X != newVector2.X) && (current.Y != newVector2.Y))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (!CheckOccupied(fieldsStatus, current))
                    return false;
            }
            return !(fieldsStatus[newVector2.X, newVector2.Y].Figure?.WhiteColor == color);
        }
        public static void AttackFields(FieldsStatus[,] fieldsStatus, Vector2 current, Vector2 direction, bool[,] newAttackedFields)
        {
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));

            while (current.X < 8 || current.X > 0 || current.Y < 8 || current.Y > 0)
            {
                current.X += step.X;
                current.Y += step.Y;

                if (!SetAttackFields(fieldsStatus, newAttackedFields, current))
                    break;
            }
        }
        public static bool[,] SetNewAttackFields(IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus)
        {
            bool[,] newAttackFields = new bool[Board.X, Board.Y];
            foreach (var f in attackingFigures)
            {
                f.SetAttackFields(fieldsStatus, newAttackFields);
            }
            return newAttackFields;
        }
        public static void SetAttackingFigures(IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 king)
        {
            foreach (var f in attackingFigures)
            {
                bool[,] newAttackFields = new bool[Board.X, Board.Y];
                f.SetAttackFields(fieldsStatus, newAttackFields);
                if (newAttackFields[king.X, king.Y])
                    f.IsAttacking = true;
            }
        }
        public static bool CheckRevealAttack(Figure figure, FieldsStatus[,] fieldsStatus, 
            IEnumerable<Figure> attackingFigures, Vector2 kingVector2)
        {
            var _figure = fieldsStatus[figure.Vector2.X, figure.Vector2.Y].Figure;
            fieldsStatus[figure.Vector2.X, figure.Vector2.Y].Figure = null;
            var newAttackFields = SetNewAttackFields(attackingFigures, fieldsStatus);
            fieldsStatus[figure.Vector2.X, figure.Vector2.Y].Figure = _figure;
            return newAttackFields[kingVector2.X, kingVector2.Y];
        }
        private static bool SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            newAttackedFields[vector2.X, vector2.Y] = true;
            return fieldsStatus[vector2.X, vector2.Y].Figure is not null;
        }
        public static bool CheckCover(Vector2 vector2, IEnumerable<Figure> defendingFigures, IEnumerable<Figure> attackingFigures,
            FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            foreach (var f in defendingFigures)
            {
                if (CheckRevealAttack(f, fieldsStatus, attackingFigures, kingVector2))
                    return false;
                if (f.ChcekLegalMovement(fieldsStatus, vector2))
                    return true;
            }
            return false;
        }
        internal static bool CheckOccupied(FieldsStatus[,] fieldsStatus, Vector2 current)
            => fieldsStatus[current.X, current.Y].Figure is null;
    }
}
