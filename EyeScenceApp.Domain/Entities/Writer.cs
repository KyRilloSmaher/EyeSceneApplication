using System.ComponentModel.DataAnnotations;
using EyeScenceApp.Domain.Enums;

namespace EyeScenceApp.Domain.Entities
{
    public class Writer : Crew
    {

        public bool IsScreenwriter { get; set; }

        public WritingStyle WritingStyle { get; set; }
    }

}
