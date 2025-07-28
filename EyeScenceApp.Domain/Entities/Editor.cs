using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using EyeScenceApp.Domain.Enums;
namespace EyeScenceApp.Domain.Entities
{

    public class Editor : Crew
    {
            public int EditedProjectsCount { get; set; }

            public EditingTechniques EditingTechniques { get; set; }

     }

}
