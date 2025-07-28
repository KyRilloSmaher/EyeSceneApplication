using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards
{
    public class CreateCelebirtyAwardDTO
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile Poster { get; set; }
        public DateTime AwardedDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
    }
}
