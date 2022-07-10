using AutoMapper;
using ChesApi.Services.PrivateDto;
using Chess.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Services.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LiveGame, PrivateLiveGameDto>();
            })
            .CreateMapper();
    }
}
