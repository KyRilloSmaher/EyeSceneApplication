using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EyeScenceApp.Domain.Entities
{
  

    public class WorksOn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// The ID of the digital content.
        /// </summary>
        public Guid DigitalContentId { get; set; }

        [ForeignKey(nameof(DigitalContentId))]
        public DigitalContent DigitalContent { get; set; }

        /// <summary>
        /// The ID of the crew.
        /// </summary>
        public Guid CrewId { get; set; }

        [ForeignKey(nameof(CrewId))]
        public Crew Crew { get; set; }
 
    }

}
