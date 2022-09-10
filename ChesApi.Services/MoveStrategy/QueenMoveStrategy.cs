using ChesApi.Infrastructure.Services.EnumFiguresDirection;
using ChesApi.Infrastructure.Services.MoveStrategy.MoveDirectionStrategy.SelectLogic;
using Chess.Core.Domain;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.static_methods;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services.MoveStrategy
{
    public class QueenMoveStrategy : IFigureTypeMoveStrategy
    {
        private readonly IFigureRepository _figureRepository;
        public QueenMoveStrategy(IFigureRepository figureRepository)
        {
            _figureRepository = figureRepository;
        }
        public void Move(Figure figure, LiveGame liveGame, Vector2 newVector2, EnumDirection enumDirection, IEnumerable<Figure> enemyFigures)
        {
            Figure king = _figureRepository.GetKing(liveGame, figure.Color);

            var attackingFigures = enemyFigures.Where(x => x.Vector2.X != newVector2.X && x.Vector2.Y != newVector2.Y);
            var attackFieldsAftreMove = StaticMoveLogicMethods.SceneAttackCheck(figure, liveGame.FieldsStatus, attackingFigures);
            switch (enumDirection)
            {
                case EnumDirection.Up:
                    {
                        if (!StaticMoveLogicMethods.UpMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Down:
                    {
                        if (!StaticMoveLogicMethods.DownMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Left:
                    {
                        if (!StaticMoveLogicMethods.LeftMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.Right:
                    {
                        if (!StaticMoveLogicMethods.RightMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.UpLeft:
                    {
                        if (!StaticMoveLogicMethods.UpLeftMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.UpRight:
                    {
                        if (!StaticMoveLogicMethods.UpRightMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.DownLeft:
                    {
                        if (!StaticMoveLogicMethods.DownLeftMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
                case EnumDirection.DownRight:
                    {
                        if (!StaticMoveLogicMethods.DownRightMovement(figure.Vector2, newVector2, liveGame.FieldsStatus))
                            throw new InvalidOperationException();
                        if (StaticMoveLogicMethods.CheckSetNewPosition
                            (figure.Color, liveGame.FieldsStatus, newVector2, king.Vector2, attackFieldsAftreMove))
                            figure.SetNewPosition(newVector2);
                        else
                            throw new InvalidOperationException();

                        break;
                    }
            }
        }
    }
}
