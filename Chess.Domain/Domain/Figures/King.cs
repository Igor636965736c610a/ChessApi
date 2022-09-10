using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain.Figures
{
    public class King : Figure
    {
        public King(Value value, FigureColor color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.King;
        }

        public override bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2, EnumDirection enumDirection)
        {
            throw new NotImplementedException();
        }

        public override bool CheckCheckamte(Vector2 newVector2, IEnumerable<Figure> defendingFigures, IEnumerable<Figure> attackingFigures, FieldsStatus[,] fieldsStatus, Vector2 kingVector2, EnumDirection direction)
        {
            throw new NotImplementedException();
        }

        public override bool CheckLegalMoveDirection(Vector2 newVector2)
        {
            throw new NotImplementedException();
        }

        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            throw new NotImplementedException();
        }

        public override EnumDirection SetDirection(Vector2 newVector2)
        {
            throw new NotImplementedException();
        }
    }
}
