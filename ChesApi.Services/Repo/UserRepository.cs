using ChesApi.Services.MyContext;
using Chess.Core.Domain;
using Chess.Core.Repo.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly ChessApiContext _context;
        public UserRepository(ChessApiContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<User>> GetUsersByName(string name)
            => await Task.FromResult(_context.Users.Where(user => user.Name == name));

        public async Task<User> GetUserByUsername(string username)
            => await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

        public async Task<User> GetUserByEmail(string email)
            => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User> GetUserById(Guid id)
            => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<User>> GetAllUsers()
            => await Task.FromResult(_context.Users);

        public async Task UpdateUser(User user)
            => await Task.FromResult(_context.Users.Update(user));

        public async Task RemoveUser(User user)
        {
            await Task.FromResult(_context.Users.Remove(user));
        }
    }
}
