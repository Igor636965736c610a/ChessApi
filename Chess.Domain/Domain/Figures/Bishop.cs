using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public class Bishop : Figure
    {
        public Bishop(Value value, bool color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Bishop;
        }

        public override bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2)
        {
            if (!CheckDirectionValid(newVector2))
                return false;
            var direction = new Vector2(newVector2.X - Vector2.X, newVector2.Y - Vector2.Y);

            return UtilsMethods.LegalMovement(fieldsStatus, Vector2, newVector2, direction, WhiteColor);
        }
        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            UtilsMethods.AttackFields(fieldsStatus, Vector2, new Vector2(1, 1), newAttackedFields);
            UtilsMethods.AttackFields(fieldsStatus, Vector2, new Vector2(-1, -1), newAttackedFields);
            UtilsMethods.AttackFields(fieldsStatus, Vector2, new Vector2(-1, 1), newAttackedFields);
            UtilsMethods.AttackFields(fieldsStatus, Vector2, new Vector2(1, -1), newAttackedFields);
        }
        private bool CheckDirectionValid(Vector2 newVector2)
            => Math.Abs(Vector2.X - newVector2.X) == Math.Abs(Vector2.Y - newVector2.Y);
    }
}
