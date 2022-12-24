using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.DTO
{
    public class LiveGameDTO
    {
        public BoardDTO Board { get; set; }
        public Guid Id { get; set; } 
        public PlayerDTO Player1 { get; set; }
        public PlayerDTO Player2 { get; set; }
        public bool WhiteColor { get; set; } = true;
    }
}
