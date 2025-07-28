using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Series
{
    public class SeriesDTO : DigitalContentDTO
    {

        public int SeasonsCount { get; set; }
        public int EpisodesCount { get; set; }
    }
}
