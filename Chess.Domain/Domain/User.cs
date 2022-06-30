using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Domain
{
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Id { get; set; }
        public LiveGame LiveGame { get; set; }
        public FigureColour FigureColour { get; set; }
        public User(string name, string email, string password, Guid id)
        {
            Name = name;
            Email = email;
            Password = password;
            Id = id;
        }
        public User(string name, string email, string password, Guid id, LiveGame liveGame, FigureColour figureColour)
        {
            Name = name;
            Email = email;
            Password = password;
            Id = id;
            LiveGame = liveGame;
            FigureColour = figureColour;
        }
    }
}
