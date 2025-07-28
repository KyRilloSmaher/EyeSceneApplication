using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.DigitalContent
{
    public class AddCrewMemberFromDigitalContentCastRequest
    {
        public Guid DigitalContentId { get; set; }
        public Guid CrewId { get; set; }

    }
}
