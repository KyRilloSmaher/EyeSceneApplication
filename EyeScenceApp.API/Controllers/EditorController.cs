using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Editiors;
using EyeScenceApp.Application.Services;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class EditorController : ControllerBase
    {
        private readonly IEditorService _EditorService;
        public EditorController(IEditorService EditorService)
        {
            _EditorService = EditorService;
        }

        [HttpGet(Router.Editor.GetById)]
        public async Task<IActionResult> GetEditorByIdAsync([FromRoute] Guid Id)
        {

            var response = await _EditorService.GetEditorByIdAsync(Id);
            return Router.FinalResponse<EditorDTO>(response);
        }
        [HttpGet(Router.Editor.GetAll)]
        public async Task<IActionResult> GetAllEditorsAsync()
        {

            var response = await _EditorService.GetAllEditorsAsync();
            return Router.FinalResponse<ICollection<EditorDTO>>(response);
        }
        [HttpGet(Router.Editor.FilterEditors)]
        public async Task<IActionResult> FilterEditors([FromQuery] FilterDTO dto)
        {

            var response = await _EditorService.FilterEditorAsync(dto);
            return Router.FinalResponse<ICollection<EditorDTO>>(response);
        }
        [HttpGet(Router.Editor.GetEditorsAwards)]
        public async Task<IActionResult> GetEditorAwards([FromRoute] Guid Id)
        {

            var response = await _EditorService.GetEditorAwardsAsync(Id);
            return Router.FinalResponse<ICollection<CelebirtyAwardDTO>>(response);
        }
        [HttpGet(Router.Editor.GetEditorsWorks)]
        public async Task<IActionResult> GetEditorWorks([FromRoute] Guid Id)
        {

            var response = await _EditorService.GetEditorWorksAsync(Id);
            return Router.FinalResponse<ICollection<DigitalContentDTO>>(response);
        }
        [HttpPost(Router.Editor.CreateEditor)]
        public async Task<IActionResult> CreateEditorAsync([FromBody] CreateEditorDTO dto)
        {

            var response = await _EditorService.CreateEditorAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpPut(Router.Editor.UpdateEditor)]
        public async Task<IActionResult> UpdateEditorAsync([FromBody] UpdateEditorDTO dto)
        {

            var response = await _EditorService.UpdateEditorAsync(dto);
            return Router.FinalResponse<bool>(response);
        }
        [HttpDelete(Router.Editor.DeleteEditor)]
        public async Task<IActionResult> DeleteEditorAsync([FromRoute] Guid Id)
        {

            var response = await _EditorService.DeleteEditorAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
