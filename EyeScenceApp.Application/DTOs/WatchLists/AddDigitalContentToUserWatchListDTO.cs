using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.WatchList
{
    public class AddDigitalContentToUserWatchListDTO
    {
        public string UserId { get; set; }
        public Guid DigitalContentId { get; set; }
    }
}
