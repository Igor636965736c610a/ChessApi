using Chess.Core.Domain.DefaultConst;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.static_methods
{
    public static class StaticMoveLogicMethods
    {
        public static void UpSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.Y + 1; i < 8; i++)
            {
                newAttackedFields[vector2.X, i] = true;
                if (fieldsStatus[vector2.X, i].OccupiedBlackFields || fieldsStatus[vector2.X, i].OccupiedWhiteFields)
                    break;
            }
        }
        public static void DownSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.Y - 1; i >= 0; i--)
            {
                newAttackedFields[vector2.X, i] = true;
                if (fieldsStatus[vector2.X, i].OccupiedBlackFields || fieldsStatus[vector2.X, i].OccupiedWhiteFields)
                    break;
            }
        }
        public static void LeftSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.X - 1; i >= 0; i--)
            {
                newAttackedFields[i, vector2.Y] = true;
                if (fieldsStatus[i, vector2.Y].OccupiedBlackFields || fieldsStatus[i, vector2.Y].OccupiedWhiteFields)
                    break;
            }
        }
        public static void RightSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.X + 1; i < 8; i++)
            {
                newAttackedFields[i, vector2.Y] = true;
                if (fieldsStatus[i, vector2.Y].OccupiedBlackFields || fieldsStatus[i, vector2.Y].OccupiedWhiteFields)
                    break;
            }
        }
        public static void UpRightSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.Y + 1; i < 8; i++)
            {
                for (int z = vector2.X + 1; z < 8; z++)
                {
                    newAttackedFields[z, i] = true;
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        break;
                }
            }
        }
        public static void UpLeftSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.Y + 1; i < 8; i++)
            {
                for (int z = vector2.X - 1; z >= 0; z--)
                {
                    newAttackedFields[z, i] = true;
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        break;
                }
            }
        }
        public static void DownRightSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.Y - 1; i >= 0; i--)
            {
                for (int z = vector2.X + 1; z < 8; z++)
                {
                    newAttackedFields[z, i] = true;
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        break;
                }
            }
        }
        public static void DownLeftSetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields, Vector2 vector2)
        {
            for (int i = vector2.Y - 1; i >= 0; i--)
            {
                for (int z = vector2.X - 1; z >= 0; z--)
                {
                    newAttackedFields[z, i] = true;
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        break;
                }
            }
        }
        // bishop

        public static bool UpMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.Y + 1; i < newVector2.Y; i++)
            {
                if (fieldsStatus[newVector2.X, i].OccupiedBlackFields || fieldsStatus[newVector2.X, i].OccupiedWhiteFields)
                    return false;
            }
            return true;
        }
        public static bool DownMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.Y - 1; i > newVector2.Y; i--)
            {
                if (fieldsStatus[newVector2.X, i].OccupiedBlackFields || fieldsStatus[newVector2.X, i].OccupiedWhiteFields)
                    return false;
            }
            return true;
        }
        public static bool LeftMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.X - 1; i > newVector2.X; i--)
            {
                if (fieldsStatus[i, newVector2.Y].OccupiedBlackFields || fieldsStatus[i, newVector2.Y].OccupiedWhiteFields)
                    return false;
            }
            return true;
        }
        public static bool RightMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.X + 1; i < newVector2.X; i++)
            {
                if (fieldsStatus[i, newVector2.Y].OccupiedBlackFields || fieldsStatus[i, newVector2.Y].OccupiedWhiteFields)
                    return false;
            }
            return true;
        }
        public static bool UpRightMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.Y + 1; i < newVector2.Y; i++)
            {
                for (int z = oldVector2.X + 1; z < newVector2.X; z++)
                {
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        return false;
                }
            }
            return true;
        }
        public static bool UpLeftMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.Y + 1; i < newVector2.Y; i++)
            {
                for (int z = oldVector2.X - 1; z > newVector2.X; z--)
                {
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        return false;
                }
            }
            return true;
        }
        public static bool DownRightMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.Y - 1; i > newVector2.Y; i--)
            {
                for (int z = oldVector2.X + 1; z < newVector2.X; z++)
                {
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        return false;
                }
            }
            return true;
        }
        public static bool DownLeftMovement(Vector2 oldVector2, Vector2 newVector2, FieldsStatus[,] fieldsStatus)
        {
            for (int i = oldVector2.Y - 1; i > newVector2.Y; i--)
            {
                for (int z = oldVector2.X - 1; z > newVector2.X; z--)
                {
                    if (fieldsStatus[z, i].OccupiedBlackFields || fieldsStatus[z, i].OccupiedWhiteFields)
                        return false;
                }
            }
            return true;
        }
        // BishopMoves
        // KingMoves 
        public static bool UpAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.Y; i < newVector2.Y; i++)
            {
                if (CheckCover(new Vector2(oldVector2.X, i), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                    return true;
            }
            return false;
        }
        public static bool DownAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.Y; i > newVector2.Y; i--)
            {
                if (CheckCover(new Vector2(oldVector2.X, i), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                    return true;
            }
            return false;
        }
        public static bool LeftAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.X; i > newVector2.X; i--)
            {
                if (CheckCover(new Vector2(i, oldVector2.Y), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                    return true;
            }
            return false;
        }
        public static bool RightAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.X; i < newVector2.X; i++)
            {
                if (CheckCover(new Vector2(i, oldVector2.Y), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                    return true;
            }
            return false;
        }
        public static bool UpLeftAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.Y; i < newVector2.Y; i++)
            {
                for (int z = oldVector2.X; z > newVector2.X; z--)
                {
                    if (CheckCover(new Vector2(z, i), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                        return true;
                }
            }
            return false;
        }
        public static bool UpRightAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.Y; i < newVector2.Y; i++)
            {
                for (int z = oldVector2.X; z < newVector2.X; z++)
                {
                    if (CheckCover(new Vector2(z, i), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                        return true;
                }
            }
            return false;
        }
        public static bool DownLeftAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.Y; i > newVector2.Y; i--)
            {
                for (int z = oldVector2.X; z > newVector2.X; z--)
                {
                    if (CheckCover(new Vector2(z, i), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                        return true;
                }
            }
            return false;
        }
        public static bool DownRightAttack(Vector2 oldVector2, Vector2 newVector2, IEnumerable<Figure> defendingFigures,
            IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            for (int i = oldVector2.Y; i > newVector2.Y; i--)
            {
                for (int z = oldVector2.X; z < newVector2.X; z++)
                {
                    if (CheckCover(new Vector2(z, i), defendingFigures, attackingFigures, fieldsStatus, kingVector2))
                        return true;
                }
            }
            return false;
        }
        public static bool CheckSetNewPosition(FigureColor figureColor, FieldsStatus[,] fieldsStatus, Vector2 vector2,
            Vector2 kingVector2, bool[,] attackEnemyFieldsAfterThisMove)
        {
            if (figureColor == FigureColor.White)
            {
                if (fieldsStatus[vector2.X, vector2.Y].OccupiedWhiteFields)
                    return false;

                return !attackEnemyFieldsAfterThisMove[kingVector2.X, kingVector2.Y];;
            }
            if (figureColor == FigureColor.Black)
            {
                if (fieldsStatus[vector2.X, vector2.Y].OccupiedBlackFields)
                    return false;

                return !attackEnemyFieldsAfterThisMove[kingVector2.X, kingVector2.Y];
            }
            throw new InvalidOperationException();
        }
        private static bool CheckCover(Vector2 vector2, IEnumerable<Figure> defendingFigures, IEnumerable<Figure> attackingFigures,
            FieldsStatus[,] fieldsStatus, Vector2 kingVector2)
        {
            foreach (var f in defendingFigures)
            {
                bool[,] attackEnemyFieldsAfterThisMove = SceneAttackCheck(f, fieldsStatus, attackingFigures);
                if (f.CheckLegalMoveDirection(vector2))
                {
                    var direction = f.SetDirection(vector2);
                    if (f.ChcekLegalMovement(fieldsStatus, vector2, direction) && CheckSetNewPosition(f.Color,
                        fieldsStatus, vector2, kingVector2, attackEnemyFieldsAfterThisMove))
                        return true;
                }
            }
            return false;
        }
        public static bool[,] SceneAttackCheck(Figure figure, FieldsStatus[,] fieldsStatus, IEnumerable<Figure> attackingFigures)
        {
            if(figure.Color == FigureColor.White)
            {
                fieldsStatus[figure.Vector2.X, figure.Vector2.Y].OccupiedWhiteFields = false;
                var newAttackFields = SetNewAttackFields(attackingFigures, fieldsStatus);
                fieldsStatus[figure.Vector2.X, figure.Vector2.Y].OccupiedWhiteFields = true;
                return newAttackFields;
            }
            if (figure.Color == FigureColor.Black)
            {
                fieldsStatus[figure.Vector2.X, figure.Vector2.Y].OccupiedBlackFields = false;
                var newAttackFields = SetNewAttackFields(attackingFigures, fieldsStatus);
                fieldsStatus[figure.Vector2.X, figure.Vector2.Y].OccupiedBlackFields = true;
                return newAttackFields;
            }
            throw new InvalidOperationException();
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
    }
}
