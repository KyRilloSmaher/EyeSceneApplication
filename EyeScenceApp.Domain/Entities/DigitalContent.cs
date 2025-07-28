using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EyeScenceApp.Domain.Enums;
namespace EyeScenceApp.Domain.Entities
{
    public  class DigitalContent
    {
      
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2500, MinimumLength = 10)]
        public string ShortDescription { get; set; }    = string.Empty;

        [Required]
        public Nationality CountryOfOrigin { get; set; }

        [Required]
        [Range(30, 240, ErrorMessage = "Duration must be between 30 and 240 minutes.")]
        public int DurationByMinutes { get; set; }

        [Required]
        [Range(1900, 2025, ErrorMessage = "Release year must be between 1900 and 2025.")]
        public int ReleaseYear { get; set; }

        [Required]
        public Guid PosterId { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Rate must be between 0 and 5.")]
        public double Rate { get; set; } = 0;

        [Required]
        public DateTime UploadingDate { get; set; } = DateTime.UtcNow;

        public Image Image { get; set; } 
        public ICollection<Rate> Ratings { get; set; }
        public ICollection<DigitalContentGenres> Genres { get; set; }
        public ICollection<Favorite> FavoriteMovies { get; set; }
        public ICollection<WatchList> WatchListMovies { get; set; }
        public ICollection<MovieCast> MovieCasts { get; set; }
        public ICollection<WorksOn> WorksOn { get; set; }
        public ICollection<DigitalContentAwards> Awards { get; set; }
    }


}
