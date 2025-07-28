using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Series
{
    public class CreateSeriesDTO
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public Nationality CountryOfOrigin { get; set; }
        public int ReleaseYear { get; set; }
        public IFormFile Poster { get; set; }
        public int SeasonsCount { get; set; }
        public int EpisodesCount { get; set; }
    }
}
