using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Commands.User
{
    public class LoginUser : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
