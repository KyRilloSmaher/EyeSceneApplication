using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.Entities
{
    public class DigitalContentGenres
    {
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
        
        public Guid DigitalContentId { get; set; }
        public DigitalContent DigitalContent { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}