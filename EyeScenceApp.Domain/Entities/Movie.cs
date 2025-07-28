

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EyeScenceApp.Domain.Entities
{
    public class Movie : DigitalContent
    {
        public int Revenues { get; set; }
    }
}
