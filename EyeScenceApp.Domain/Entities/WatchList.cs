using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeScenceApp.Domain.Entities
{
    public class WatchList
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        public Guid DigitalContentId { get; set; }
        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        [ForeignKey(nameof(DigitalContentId))]
        public DigitalContent DigitalContent { get; set; }
    }
}
