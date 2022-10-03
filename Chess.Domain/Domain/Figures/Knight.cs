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
    public class Knight : Figure
    {
        public Knight(Value value, FigureColor color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Knight;
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
            => Math.Abs(Vector2.X - newVector2.X) * Math.Abs(Vector2.Y - newVector2.Y) == 2;

        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            throw new NotImplementedException();
        }

        public override EnumDirection SetDirection(Vector2 newVector2)
            => (EnumDirection)(Vector2.Y > newVector2.Y ? 0 : 1) + ((Vector2.X > newVector2.X ? 0 : 1) << 1) + ((Math.Abs(Vector2.X - newVector2.X) == 2 ? 0 : 1) << 2) + 8;

        private enum KngihtDirection
        {
            DownLeft = 1,
            UpLeft = 2,
            DownRight = 3,
            UpRight = 4,
            LeftDown = 5,
            LeftUp = 6,
            RightDown = 7,
            RightUp = 8,
        }
    }
}
