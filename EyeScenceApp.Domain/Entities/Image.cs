using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsPrimary { get; set; } = false;
        public Award award { get; set; } = new Award();
        public Genre genre { get; set; } = new Genre();
        public DigitalContent DigitalContent { get; set; }
        public Episode Episode { get; set; }
        public ICollection<CelebirtyImages> CelebirtyImages { get; set; } = new HashSet<CelebirtyImages>();
       
    }
}
