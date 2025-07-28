using System.ComponentModel.DataAnnotations;
using EyeScenceApp.Domain.Enums;
namespace EyeScenceApp.Domain.Entities
{
    public class Actor : Cast
    {
        
        public int TotalMovies { get; set; }

        public bool IsCurrentlyActive { get; set; }

        public ActingStyle ActingStyle { get; set; }

        public ICollection<MovieCast> MovieCasts { get; set; } = new HashSet<MovieCast>();

    }

}
