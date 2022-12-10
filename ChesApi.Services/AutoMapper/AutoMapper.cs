using AutoMapper;
using ChesApi.Infrastructure.DTO;
using Chess.Core.Domain;
using Chess.Core.Domain.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Figure, FigureDTO>();
                cfg.CreateMap<Board, BoardDTO>();
                cfg.CreateMap<LiveGame, LiveGameDTO>();
                cfg.CreateMap<Player, PlayerDTO>();
            })
            .CreateMapper();
    }
}
