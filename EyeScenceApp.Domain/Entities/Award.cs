using System.Numerics;

namespace EyeScenceApp.Domain.Entities
{
    public class Award
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid PosterId { get; set; }
        public DateTime AwardedDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public Image Image { get; set; }
    }
}
