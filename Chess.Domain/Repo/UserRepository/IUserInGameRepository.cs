using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Repo.UserRepository
{
    public interface IUserInGameRepository
    {
        void AddUser(User user);
        User GetUser(Guid id);
    }
}
