using EyeScenceApp.Application.DTOs.Images;
using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Actors
{
    public class CreateActorDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public IFormFile PrimaryImage { get; set; }
        public ICollection<IFormFile>? Images { get; set; } = new HashSet<IFormFile>();
        public DateTime BirthDate { get; set; }
        public Nationality Nationality { get; set; }
        public Gender Sex { get; set; }
        public int TotalMovies { get; set; }
        public bool IsCurrentlyActive { get; set; }
        public ActingStyle ActingStyle { get; set; }
    }
}
