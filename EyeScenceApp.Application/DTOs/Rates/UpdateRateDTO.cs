using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Rates
{
    public class UpdateRateDTO
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public string? Review { get; set; }

    }
}
