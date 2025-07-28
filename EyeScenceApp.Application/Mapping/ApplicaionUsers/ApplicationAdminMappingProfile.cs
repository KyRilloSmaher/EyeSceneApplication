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
    public class ApplicationAdminMappingProfile : Profile
    {
        public ApplicationAdminMappingProfile() { 
         CreateMap<ApplicationUser ,ApplicationAdminDTO>();
        }
    }
}
