using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.SingleDocumentries
{
    public class UpdateDocumantryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public Nationality CountryOfOrigin { get; set; }

        public int DurationByMinutes { get; set; }

        public int ReleaseYear { get; set; }

        public IFormFile? Poster { get; set; }

    }
}
