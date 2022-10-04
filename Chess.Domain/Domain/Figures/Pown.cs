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
    public class Pown : Figure
    {
        public bool EnPassant { get; protected set; } = false;
        public Pown(Value value, bool color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Pown;
        }

        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            throw new NotImplementedException();
        }
        public override bool ChcekLegalMovement(FieldsStatus[,] fieldsStatus, Vector2 newVector2)
        {
            throw new NotImplementedException();
        }
        public override void SetNewPosition(Vector2 newVector2)
        {

        }
        private bool CheckDirectionValid(Vector2 newVector2)
        {

        }
    }
}
