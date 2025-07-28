using AutoMapper;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.ApplicaionUsers
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMapBetweenApplicationUserAndUserDTO();
            CreateMapBetweenCreateDTOAndApplicationUser();
            CreateMapBetweenUpdateDTOAndApplicationUser();
        }

        private void CreateMapBetweenApplicationUserAndUserDTO()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }

        private void CreateMapBetweenCreateDTOAndApplicationUser()
        {
            CreateMap<CreateApplicationUserDTO, ApplicationUser>();
        }

        private void CreateMapBetweenUpdateDTOAndApplicationUser()
        {
            CreateMap<UpdateUserDTO, ApplicationUser>()
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}

