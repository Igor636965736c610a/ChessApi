using Chess.Core.Domain;
using Chess.Core.Repo.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Repo
{
    public class UserInGameRepository : IUserInGameRepository
    {
        public List<User> _users { get; set; } = new List<User>();

        public void AddUser(User user)
            => _users.Add(user);

        public User GetUserById(Guid id)
            => _users.FirstOrDefault(x => x.Id == id);
    }
}
