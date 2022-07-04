using Chess.Core.Domain.Enums;
using Chess.Core.Domain.DefaultConst;
using Chess.Core.Repo.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Core.Repo.Game;

namespace ChesApi.Services.Services
{
    public class MoveService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserInGameRepository _userInGameRepository;
        private readonly IGameRepository _gameRepository;
        public MoveService
            (IUserRepository userRepository, IUserInGameRepository userInGameRepository,
            IGameRepository GameRepository)
        {
            _userRepository = userRepository;
            _userInGameRepository = userInGameRepository;
            _gameRepository = GameRepository;
        }

        public void Move(int x, int y, Guid userId, Guid figureId)
        {
            if (x == null || y == null || userId == null || figureId == null)
            {
                throw new NullReferenceException();
            }
            if (x > Board.X || x < 1 || y > Board.Y || y < 1)
            {
                throw new Exception("X or Y must by bigger than 0 and lower than 9");
            }
            var user = _userInGameRepository.GetUserById(userId);
            if (user is null)
            {
                throw new NullReferenceException();
            }
            var liveGame = user.LiveGame;
            if(liveGame is null)
            {
                throw new Exception("404 sesion");
            }
            if(liveGame.IsGaming == false)
            {
                throw new InvalidOperationException();
            }
            var figure = _gameRepository.GetFigure(liveGame, figureId);
            if(figure is null)
            {
                throw new NullReferenceException();
            }
            switch (figure.FigureType)
            {
                case FigureType.Queen:
                    {
                        break;
                    }
                case FigureType.Knight:
                    {
                        break;
                    }
                case FigureType.Pown:
                    {
                        break;
                    }
                case FigureType.King:
                    {
                        break;
                    }
                case FigureType.Bishop:
                    {
                        break;
                    }
                case FigureType.Rock:
                    {
                        break;
                    }
            }
        }
    }
}
