using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Editiors;
using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IEditorService
    {
        public Task<Response<EditorDTO?>> GetEditorByIdAsync(Guid EditorId);
        public Task<Response<ICollection<EditorDTO>>> GetAllEditorsAsync();
        public Task<Response<ICollection<EditorDTO>>> FilterEditorAsync(FilterDTO dto);
        public Task<Response<ICollection<CelebirtyAwardDTO>>> GetEditorAwardsAsync(Guid EditorId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetEditorWorksAsync(Guid EditorId);
        public Task<Response<bool>> UpdateEditorAsync(UpdateEditorDTO EditorDTO);
        public Task<Response<bool>> CreateEditorAsync(CreateEditorDTO EditorDTO);
        public Task<Response<bool>> DeleteEditorAsync(Guid EditorId);
    }
}
