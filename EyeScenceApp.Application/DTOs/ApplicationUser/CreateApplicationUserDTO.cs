using EyeScenceApp.Domain.Enums;

namespace EyeScenceApp.Application.DTOs.ApplicationUser
{
    public class CreateApplicationUserDTO
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public Nationality Nationality {get ;set;} 
        public int Age { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}