using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.Entities
{
    public class CelebirtyImages
    {
        public Guid ImageId { get; set; }
        public Guid CelebirtyId { get; set; }
        public Celebrity Celebrity { get; set; } 
        public Image Image { get; set; } 
    }
}
