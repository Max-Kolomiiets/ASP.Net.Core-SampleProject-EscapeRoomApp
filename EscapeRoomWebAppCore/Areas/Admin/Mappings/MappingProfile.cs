using AutoMapper;
using EscapeRoomWebAppCore.Areas.Admin.DTOs;
using EscapeRoomWebAppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Areas.Admin.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EscapeRoomDTO, EscapeRoom>()
                .ForMember(m => m.Logotype, op => op.MapFrom(p => p.LogoBytes));

            CreateMap<EscapeRoom, EscapeRoomDTO>()
                .ForMember(p => p.LogoBytes, op => op.MapFrom(p => p.Logotype))
                .ForMember(p => p.Logotype, op => op.Ignore());
        }
    }
}
