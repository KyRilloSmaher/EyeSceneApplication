using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.DigitalContent
{
    public class DeleteAwardFromDigitalContentRequest
    {
        public Guid DigitalContentId { get; set; }
        public Guid AwardId { get; set; }
        public int Year { get; set; }
      
    }
}
