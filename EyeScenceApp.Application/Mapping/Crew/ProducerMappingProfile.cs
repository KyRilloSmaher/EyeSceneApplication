using AutoMapper;
using EyeScenceApp.Application.DTOs.Crew.Producers;
using EyeScenceApp.Domain.Entities;

namespace EyeScenceApp.Application.Mapping.Crew
{
    public class ProducerMappingProfile :Profile
    {

        public ProducerMappingProfile()
        {
            configureMappingBetweenCreateProducerandProducer();
            configureMappingBetweenProducerandProducerDTO();
            configureMappingBetweenUpdateProducerandProducer();
        }


        void configureMappingBetweenProducerandProducerDTO()
        {

            CreateMap<Producer, ProducerDTO>()
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Image.Url)));
        }

        void configureMappingBetweenCreateProducerandProducer()
        {

            CreateMap<CreateProducerDTO, Producer>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;

        }

        void configureMappingBetweenUpdateProducerandProducer()
        {
            CreateMap<UpdateProducerDTO, Producer>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
        }
    }
}
