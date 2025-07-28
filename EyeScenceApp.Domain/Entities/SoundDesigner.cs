using System.ComponentModel.DataAnnotations;

namespace EyeScenceApp.Domain.Entities
{
    public class SoundDesigner : Crew
    {
    
     [StringLength(500)]
      public string KnownForSoundtracks { get; set; } = string.Empty;
  
      }
}
