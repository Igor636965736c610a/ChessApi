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
    public class Pown : Figure
    {
        private bool FirstMove { get; set; } = true;
        public Pown(Value value, bool color, Vector2 vector2, Guid id) : base(value, color, vector2, id)
        {
            FigureType = FigureType.Pown;
        }

        public override void SetAttackFields(FieldsStatus[,] fieldsStatus, bool[,] newAttackedFields)
        {
            throw new NotImplementedException();
        }
        public override bool ChcekLegalMovement(LiveGame liveGame, Vector2 newVector2)
        {
            var fieldsStatus = liveGame.FieldsStatus;
            Vector2 movement;
            Vector2 direction;
            if(!CheckDirectionValid(newVector2, out movement, out direction))
                return false;
            if(Math.Abs(movement.Y) == 2 && FirstMove == false)
                return false;
            if(Math.Abs(direction.X) == 1 && ((fieldsStatus[newVector2.X, newVector2.Y].Figure?.WhiteColor == WhiteColor)
                || (liveGame.EnPassant.CanEnPassant == true 
                && liveGame.EnPassant.Vector2.X == newVector2.X 
                && liveGame.EnPassant.Vector2.Y == newVector2.Y)))
                return false; 
            if(Math.Abs(movement.Y) == 2 && !UtilsMethods.LegalMovement(fieldsStatus, Vector2, newVector2, direction, WhiteColor))
                return false;
            return !(direction.X == 0 && fieldsStatus[newVector2.X, newVector2.Y].Figure?.WhiteColor != WhiteColor);
        }
        public override void SetNewPosition(Vector2 newVector2)
        {
            Vector2 = new Vector2(newVector2.X, newVector2.Y);
            FirstMove = false;
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

    }
}
