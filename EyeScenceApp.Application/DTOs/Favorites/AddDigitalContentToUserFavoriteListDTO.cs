using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Favorites
{
    public class AddDigitalContentToUserFavoriteListDTO
    {
        public string UserId { get; set; }
        public Guid DigitalContentId { get; set; }
    }
}
