using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.DTOs.Crew.Editiors
{
    public class CreateEditorDTO : CreateCrewDTO
    {
        public int EditedProjectsCount { get; set; }
        public EditingTechniques EditingTechniques { get; set; }
    }
}
