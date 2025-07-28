using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.ApplicationUser
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public Nationality Nationality { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
