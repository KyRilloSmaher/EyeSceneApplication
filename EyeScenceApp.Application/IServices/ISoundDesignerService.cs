using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Directors;
using EyeScenceApp.Application.DTOs.Crew.SoundDesigners;
using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface ISoundDesignerService
    {
        public Task<Response<SoundDesignerDTO?>> GetSoundDesignerByIdAsync(Guid SoundDesignerId);
        public Task<Response<ICollection<SoundDesignerDTO>>> GetAllSoundDesignersAsync();
        public Task<Response<ICollection<SoundDesignerDTO>>> FilterSoundDesignerAsync(FilterDTO dto);
        public Task<Response<ICollection<CelebirtyAwardDTO>>> GetSoundDesignerAwardsAsync(Guid SoundDesignerId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetSoundDesignerWorksAsync(Guid SoundDesignerId);
        public Task<Response<bool>> UpdateSoundDesignerAsync(UpdateSoundDesignerDTO SoundDesignerDTO);
        public Task<Response<bool>> CreateSoundDesignerAsync(CreateSoundDesignerDTO SoundDesignerDTO);
        public Task<Response<bool>> DeleteSoundDesignerAsync(Guid SoundDesignerId);
    }
}
