using AutoMapper;
using ChesApi.Infrastructure.DTO;
using Chess.Core.Domain;
using Chess.Core.Domain.Enums;
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
                cfg.CreateMap<Figure, FigureDTO>().ForMember(destination => destination.FigureType,
                    opt => opt.MapFrom(source => Enum.GetName(typeof(FigureType), source.FigureType))); 
                cfg.CreateMap<Board, BoardDTO>();
                cfg.CreateMap<LiveGame, LiveGameDTO>();
                cfg.CreateMap<Player, PlayerDTO>();
                cfg.CreateMap<Figure, GameCharDTO>();
            })
            .CreateMapper();
    }
}
