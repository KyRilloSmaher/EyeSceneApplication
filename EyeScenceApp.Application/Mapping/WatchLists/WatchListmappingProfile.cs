using AutoMapper;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Application.DTOs.WatchList;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.WatchLists
{
    public class WatchListmappingProfile : Profile
    {
        public WatchListmappingProfile()
        {
            CreateMapBetweenAddWatchListDTOandWatchList();
            CreateMapBetweenDeleteFromWatchListDTOandWatchList();
        }
    



        void CreateMapBetweenAddWatchListDTOandWatchList()
        {
            CreateMap <AddDigitalContentToUserWatchListDTO, WatchList > ()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.DigitalContentId, opt => opt.MapFrom(src => src.DigitalContentId));

        }
        void CreateMapBetweenDeleteFromWatchListDTOandWatchList()
        {
            CreateMap<DeleteDigitalContentFromUserWatchListDTO, WatchList>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.DigitalContentId, opt => opt.MapFrom(src => src.DigitalContentId));

        }
    }
}
