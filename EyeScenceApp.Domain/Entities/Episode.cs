using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EyeScenceApp.Domain.Entities
{
    public class Episode 
    {
        public Guid Id { get; set; }
        [Required]
        public  Guid posterId { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2500, MinimumLength = 10)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        [Range(30, 240, ErrorMessage = "Duration must be between 30 and 240 minutes.")]
        public int DurationByMinutes { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Season number must be a positive number.")]
        public int Season { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Episode number must be a positive number.")]
        public int EpisodeNumber { get; set; }

        public Guid SeriesId { get; set; }

        [ForeignKey(nameof(SeriesId))]
        public Series Series { get; set; }

        public Image poster { set; get; }
    }
}
