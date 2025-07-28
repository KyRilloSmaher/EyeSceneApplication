using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeScenceApp.Domain.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        [Display(Name = "Category Name ")]
        public string Name { get; set; } = string.Empty;
        public Guid PosterId { get; set; }
        public ICollection<DigitalContentGenres> contents { get; set; }
        public Image Image { set; get; }
        
    }
}
