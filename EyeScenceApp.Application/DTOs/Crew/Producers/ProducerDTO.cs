using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Crew.Producers
{
    public class ProducerDTO : CrewDTO
    {
        public int ProducedProjectsCount { get; set; }

        public decimal TotalBoxOfficeRevenue { get; set; }
    }
}
