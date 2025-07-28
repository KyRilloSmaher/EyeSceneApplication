using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Movies
{
    public class MovieDTO : DigitalContentDTO
    {
        public int Revenues { get; set; }
    }
}
