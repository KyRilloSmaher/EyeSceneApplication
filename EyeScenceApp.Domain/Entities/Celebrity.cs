using System.ComponentModel.DataAnnotations;
using EyeScenceApp.Domain.Enums;

namespace EyeScenceApp.Domain.Entities
{
    public class Celebrity
    {
     
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Bio { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public Nationality Nationality { get; set; }

        [Required]
        public Gender Sex { get; set; }

        public ICollection<CelebirtyAwards> Awards {  get; set; }
        public ICollection<CelebirtyImages> Images { get; set; } = new HashSet<CelebirtyImages>();

    }
}
