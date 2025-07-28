using AutoMapper;
using EyeScenceApp.Application.DTOs.Rates;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Rates
{
    public class RateMappingProfile : Profile
    {
        public RateMappingProfile() 
        {
            ConfigureMappingBetweenRateAndRateDTO();
            ConfigureMappingBetweenCreateRateDTOAndRate();
            ConfigureMappingBetweenUpdateRateDTOAndRate();
        }

        void ConfigureMappingBetweenRateAndRateDTO()
        {
            CreateMap<Rate, RateDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
        void ConfigureMappingBetweenCreateRateDTOAndRate()
        {
            CreateMap<CreateRateDTO, Rate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow)) // Set current date/time
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => 0)) // Initialize LikesCount to 0
                .ForMember(dest => dest.DislikesCount, opt => opt.MapFrom(src => 0)); // Initialize DislikesCount to 0
        }

        void  ConfigureMappingBetweenUpdateRateDTOAndRate()
        {
            CreateMap<UpdateRateDTO, Rate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id as it should not be set from DTO
                .ForMember(dest => dest.DigitalContentId, opt => opt.Ignore()) // Ignore DigitalContentId as it should not be set from DTO
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Ignore DigitalContentId as it should not be set from DTO
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow)) // Update date to current time
                .ForMember(dest => dest.LikesCount, opt => opt.Ignore()) // Keep LikesCount unchanged
                .ForMember(dest => dest.DislikesCount, opt => opt.Ignore()); // Keep DislikesCount unchanged
        }
    }
}
