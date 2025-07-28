using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Writers;
using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IWriterService
    {
        public Task<Response<WriterDTO?>> GetWriterByIdAsync(Guid WriterId);
        public Task<Response<ICollection<WriterDTO>>> GetAllWritersAsync();
        public Task<Response<ICollection<WriterDTO>>> FilterWriterAsync(FilterDTO dto);
        public Task<Response<ICollection<CelebirtyAwardDTO>>> GetWriterAwardsAsync(Guid WriterId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetWriterWorksAsync(Guid WriterId);
        public Task<Response<bool>> UpdateWriterAsync(UpdateWriterDTO WriterDTO);
        public Task<Response<bool>> CreateWriterAsync(CreateWriterDTO WriterDTO);
        public Task<Response<bool>> DeleteWriterAsync(Guid WriterId);
    }
}
