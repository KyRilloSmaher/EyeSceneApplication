using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Crew.Base
{
    public class FilterDTO
    {
        public bool ascendenig { get; set; } = true;
        public string? Name { get; set; } = string.Empty;
        public Nationality? nationality { get; set; }
        public Gender? sex { get; set; }
    }
}
