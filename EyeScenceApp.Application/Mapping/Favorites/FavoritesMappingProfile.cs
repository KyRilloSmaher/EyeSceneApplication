using AutoMapper;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Favorites
{
    public class FavoritesMappingProfile : Profile
    {
        public FavoritesMappingProfile() {
            CreatemapBetwennAddDTOandFavorit();

            CreatemapBetwennDeleteDTOandFavorit();

        }
        void CreatemapBetwennAddDTOandFavorit()
        {
            CreateMap<AddDigitalContentToUserFavoriteListDTO,Favorite>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.DigitalContentId, opt => opt.MapFrom(src => src.DigitalContentId));

        }
        void CreatemapBetwennDeleteDTOandFavorit()
        {
            CreateMap<DeleteDigitalContentToUserFavoriteListDTO, Favorite>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ForMember(dest => dest.DigitalContentId, opt => opt.MapFrom(src => src.DigitalContentId));

        }
    }
}
