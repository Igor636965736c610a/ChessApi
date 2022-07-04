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
    public class CreateGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepostitory;
        private readonly IUserInGameRepository _userInGameRepository;
        public CreateGameService
            (IGameRepository gameRepository, IUserRepository userRepository, IUserInGameRepository userInGameRepository)
        {
            _gameRepository = gameRepository;
            _userRepostitory = userRepository;
            _userInGameRepository = userInGameRepository;
            _userInGameRepository = userInGameRepository;
        }
        public async void CreateGame(FigureColour figureColour, Guid userId)
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
    }
}
