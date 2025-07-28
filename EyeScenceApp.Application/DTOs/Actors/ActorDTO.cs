using EyeScenceApp.Application.DTOs.Images;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Actors
{
    public class ActorDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public ICollection<string> Images { get; set; } = new HashSet<string>();
        public DateTime BirthDate { get; set; }
        public Nationality Nationality { get; set; }
        public Gender Sex { get; set; }
        public int TotalMovies { get; set; }
        public bool IsCurrentlyActive { get; set; }
        public ActingStyle ActingStyle { get; set; }
    }
}
