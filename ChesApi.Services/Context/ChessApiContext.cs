using Chess.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.MyContext
{
    public class ChessApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public ChessApiContext(DbContextOptions options) : base(options)
        {

        }
    }
}
