using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Images
{
    public class ImageDTO
    {
        public string Url { get; set; } = string.Empty;
        public bool IsPrimary { get; set; } = false;
    }
}
