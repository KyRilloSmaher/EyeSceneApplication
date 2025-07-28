using AutoMapper;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Actors
{
    public class ActorProfile : Profile
    {
        public ActorProfile() {
            CreateMapBetweenActorAndDTO();
            CreateMapBetweenActorAndUpdateActorDTO();
            CreateMapBetweenActorAndCreateActorDTO();
        }

        #region Mappings
        public void CreateMapBetweenActorAndDTO()
        {
            CreateMap<Actor, ActorDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>src.Images.Select(i => i.Image.Url)));
        }

        public void CreateMapBetweenActorAndCreateActorDTO()
        {

            CreateMap<UpdateActorDTO, Actor>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
        public void CreateMapBetweenActorAndUpdateActorDTO()
        {

            CreateMap<CreateActorDTO,Actor>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
        }
        #endregion
    }
}
