using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Rates
{
    public class RateDTO
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public string? Review { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public string UserId { get; set; } 
        public string UserName { get; set; }
        public Guid DigitalContentId { get; set; }
        public DateTime Date { get; set; }
    }
}
