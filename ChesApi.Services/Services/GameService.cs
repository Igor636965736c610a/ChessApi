using Chess.Core.Domain.Enums;
using Chess.Core.Repo.Game;
using Chess.Core.Repo.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Services
{
    public class GameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepostitory;
        private readonly IUserInGameRepository _userInGameRepository;
        public GameService
            (IGameRepository gameRepository, IUserRepository userRepository, IUserInGameRepository userInGameRepository)
        {
            _gameRepository = gameRepository;
            _userRepostitory = userRepository;
            _userInGameRepository = userInGameRepository;
        }
        public async Task CreateGame(FigureColour figureColour, Guid userId)
        {
            var user = await _userRepostitory.GetUserById(userId);
            if(user is null)
            {
                throw new NullReferenceException();
            }
            if(user.LiveGame is not null)
            {
                throw new InvalidOperationException();
            }
            user.FigureColour = figureColour;
            _gameRepository.CreategGame(user);
            _userInGameRepository.AddUser(user);
        }
        public async Task JoinToTheGame(Guid gameId, Guid userId)
        {
            var user = await _userRepostitory.GetUserById(userId);
            if (user is null)
            {
                throw new NullReferenceException();
            }
            if (user.LiveGame is not null)
            {
                throw new InvalidOperationException();
            }
            var liveGame = _gameRepository.GetLiveGame(gameId);
            if (liveGame is null)
            {
                throw new NullReferenceException();
            }
            if (liveGame.IsGaming is true)
            {
                throw new InvalidOperationException();
            }
            liveGame.User2 = user;
            user.LiveGame = liveGame;
            if(liveGame.HostUser.FigureColour is FigureColour.white)
            {
                user.FigureColour = FigureColour.black;
            }
            else
            {
                user.FigureColour = FigureColour.white;
            }
            _userInGameRepository.AddUser(user);
            liveGame.IsGaming = true;
        }
    }
}
