using EyeScenceApp.Application.DTOs.Crew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Crew.Directors
{
    public class CreateDirectorDTO : CreateCrewDTO
    {
        public int DirectedMoviesCount { get; set; }
        public string VisionStatement { get; set; }
    }
}
