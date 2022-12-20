using AutoMapper;
using ChesApi.Infrastructure.DTO;
using ChesApi.Infrastructure.Hub;
using ChesApi.Infrastructure.MoveTypeStrategy;
using ChesApi.Infrastructure.MoveTypeStrategy.Enum;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
using Chess.Core.Domain.EnumsAndStructs;
using Chess.Core.Domain.Figures;
using Chess.Core.Domain.Utils;
using Chess.Core.Repo.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IMapper _mapper;
        private readonly IHubLobby _hubLobby;
        private readonly IFigureRepository _figureRepository;
        private readonly IStrategyFactory<IStrategy> _strategyFactor;
        public GameService(IMapper mapper, IHubLobby hubLobby, IFigureRepository figureRepository
            ,IStrategyFactory<IStrategy> strategyFactory)
        {
            _mapper = mapper;
            _hubLobby = hubLobby;
            _figureRepository = figureRepository;
            _strategyFactor = strategyFactory;
        }

        public LiveGameDTO GetGameByGameId(string gameId)
        {
            var game = _hubLobby.GetGame(gameId);
            return _mapper.Map<LiveGame, LiveGameDTO>(game);
        }

        public PlayerDTO GetPlayer(string connectionId)
        {
            var player = _hubLobby.GetPlayer(connectionId);
            return _mapper.Map<Player, PlayerDTO>(player);
        }

        public GameStatus Move(MoveType moveType, Vector2 newVector2, Vector2 oldVector2, LiveGame liveGame)
        {
            if (liveGame is null)
                throw new Exception("404 sesion");

            var figure = _figureRepository.GetFigure(liveGame.Board, oldVector2);
            if (figure is null)
                throw new NullReferenceException();

            var board = liveGame.Board;
            if (newVector2.X < board.XMin && newVector2.X >= board.XMax && newVector2.Y < board.YMin && newVector2.Y >= board.YMax)
                throw new InvalidOperationException();

            if (oldVector2.X == newVector2.X && oldVector2.Y == newVector2.Y)
                throw new InvalidOperationException();

            var strategy = _strategyFactor.GetStrategy(moveType.ToString());

            var legalMoveType = strategy.Move(newVector2, figure, board);
            if (!legalMoveType)
                throw new InvalidOperationException();
            figure.SetNewPosition(newVector2, board);

            var enemyKing = _figureRepository.GetKing(board, !figure.WhiteColor);
            var playerKing = _figureRepository.GetKing(board, figure.WhiteColor);

            var attackingFigures = board.Figures
                .Where(x => x.WhiteColor == figure.WhiteColor && x.FigureType != FigureType.King)
                .Where(x => x.ChcekLegalMovement(board, enemyKing.Vector2, board.Figures.Where(x => x.WhiteColor != figure.WhiteColor)
                .ToList()))
                .ToList();

            if (attackingFigures.Count > 0)
            {
                if (CheckCheckmate(board, enemyKing, playerKing, attackingFigures))
                    return liveGame.WhiteColor ? GameStatus.WhiteCheckMate : GameStatus.BlackCheckMate;
                else
                    return liveGame.WhiteColor ? GameStatus.WhiteCheck : GameStatus.BlackCheck;
            }

            return GameStatus.IsGaming;
        }

        private bool CheckCheckmate(Board board, Figure enemyKing, Figure king, List<Figure> attackingFigures)
        {
            var dirs = king.GetDirs();
            var enemyFigures = board.Figures.Where(x => x.WhiteColor != king.WhiteColor).ToList();
            foreach (var dir in dirs)
            {
                if (king.ChcekLegalMovement(board, new Vector2(king.Vector2.X + dir.X, king.Vector2.Y + dir.Y), enemyFigures))
                    return false;
            }

            if (attackingFigures.Count() > 1)
            {
                if (attackingFigures.Any(x => x.FigureType == FigureType.Knight))
                    return true;

                if (attackingFigures.Any(x => x.FigureType == FigureType.Rook) && attackingFigures
                    .Any(x => x.FigureType == FigureType.Bishop))
                    return true;

                List<Vector2> attackDirections = attackingFigures
                    .Select(x => new Vector2(Math.Sign(enemyKing.Vector2.X - x.Vector2.X), Math.Sign(enemyKing.Vector2.Y - x.Vector2.Y)))
                    .ToList();

                if (!attackDirections.All(x => x.X == attackDirections.First().X && x.Y == attackDirections.First().Y))
                    return true;
            }
            var defendingFigures = board.Figures.Where(x => x.WhiteColor == king.WhiteColor && x.FigureType != FigureType.King);
            var firsInTheRowFigure = attackingFigures
                .OrderBy(x => Math.Abs(enemyKing.Vector2.X + enemyKing.Vector2.Y - x.Vector2.X + x.Vector2.Y))
                .First();
            var direction = new Vector2(enemyKing.Vector2.X - firsInTheRowFigure.Vector2.X, enemyKing.Vector2.Y - firsInTheRowFigure.Vector2.Y);
            var step = new Vector2(Math.Sign(direction.X), Math.Sign(direction.Y));
            var current = firsInTheRowFigure.Vector2;

            while ((current.X != enemyKing.Vector2.X) && (current.Y != enemyKing.Vector2.Y))
            {
                current.X += step.X;
                current.Y += step.Y;

                if (UtilsMethods.CheckCover(current, defendingFigures, enemyFigures, board))
                    return true;
            }
            return false;
        }
    }
}
