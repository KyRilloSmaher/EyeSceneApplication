using System.ComponentModel.DataAnnotations;

namespace EyeScenceApp.Domain.Entities
{
    public class Series : DigitalContent
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Seasons count must be a positive number.")]
        public int SeasonsCount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Episodes count must be a positive number.")]
        public int EpisodesCount { get; set; }
    }
}