using System.ComponentModel.DataAnnotations;
using EyeScenceApp.Domain.Enums;
namespace EyeScenceApp.Domain.Entities
{
    public class Director : Crew
    {
        public int DirectedMoviesCount { get; set; }

        [StringLength(500)]
        public string VisionStatement { get; set; } = string.Empty;
    }

}
