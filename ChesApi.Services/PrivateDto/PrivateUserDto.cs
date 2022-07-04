using ChesApi.Services.PrivateDto;
using Chess.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.Dto
{
    public class PrivateUserDto
    {
        public Guid Id { get; set; }
        public PrivateLiveGameDto LiveGame { get; set; }
        public FigureColour FigureColour { get; set; }
    }
}
