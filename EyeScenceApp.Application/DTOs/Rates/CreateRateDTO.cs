using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Rates
{
    public class CreateRateDTO
    {
        public double Value { get; set; }
        public string? Review { get; set; }
        public string UserId { get; set; } 
        public Guid DigitalContentId { get; set; }
    }
}
