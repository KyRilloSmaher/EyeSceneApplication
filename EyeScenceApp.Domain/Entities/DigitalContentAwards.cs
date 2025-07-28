using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeScenceApp.Domain.Entities
{
    public class DigitalContentAwards
    {
        public Guid AwardId { get; set; }
        public Guid DigitalContentId { get; set; }

        [Range(minimum: 1950, maximum: 2030)]
        public int Year { get; set; }

        [ForeignKey(nameof(DigitalContentId))]
        public DigitalContent DigitalContent { get; set; }

        [ForeignKey(nameof(AwardId))]
        public DigitalContentAward Award { get; set; }
    }
}
