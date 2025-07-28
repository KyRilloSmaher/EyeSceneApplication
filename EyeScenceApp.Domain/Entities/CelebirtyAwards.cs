using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeScenceApp.Domain.Entities
{
    public class CelebirtyAwards
    {
        public Guid AwardId { get; set; }
        public Guid Celebirtyid { get; set; }

        [Required]
        [Range(minimum: 1950, maximum: 2030)]
        public int Year { get; set; }
        [ForeignKey(nameof(Celebirtyid))]
        public Celebrity Celebrity { get; set; }
        [ForeignKey(nameof(AwardId))]
        public CelebirtyAward Award { get; set; }
    }
}
