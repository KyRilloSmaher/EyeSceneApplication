
using EyeScenceApp.Domain.Enums;

namespace EyeScenceApp.Application.DTOs.ApplicationUser
{ 
    public class UserDTO
    {
        public string Id { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Nationality Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

    }
}