using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.DigitalContent
{
    public class AddActorToDigitalContentCastRequest
    {
        public Guid DigitalContentId { get; set; }
        public Guid ActorId { get; set; }

    }
}
