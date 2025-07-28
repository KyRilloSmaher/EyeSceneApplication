using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EyeScenceApp.Domain.Entities
{
    public class Rate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating value must be between 0 and 5.")]
        public double Value { get; set; }

        [StringLength(1000, ErrorMessage = "Review cannot exceed 1000 characters.")]
        public string? Review { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

        public string UserId { get; set; } = string.Empty;
        public Guid DigitalContentId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow; // Default value

        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(DigitalContentId))]
        public DigitalContent DigitalContent { get; set; }
    }
}
