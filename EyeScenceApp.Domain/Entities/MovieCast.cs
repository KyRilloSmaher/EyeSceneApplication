using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeScenceApp.Domain.Entities
{


    public class MovieCast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid DigitalContentId { get; set; }

        [ForeignKey(nameof(DigitalContentId))]
        public DigitalContent DigitalContent { get; set; }

        public Guid ActorId { get; set; }

        [ForeignKey(nameof(ActorId))]
        public Actor Actor { get; set; }

        [Required]
        [MaxLength(500)]
        public string RoleName { get; set; } = string.Empty;
    }


}
