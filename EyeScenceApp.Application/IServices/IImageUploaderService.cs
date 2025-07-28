using CloudinaryDotNet.Actions;
using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IImageUploaderService
    {
        Task<string> UploadImageAsync(Stream fileStream, string fileName);
        Task<bool> DeleteImageAsync(string publicId);
        Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile, ImageFolder imageType);
        Task<bool> DeleteImageByUrlAsync(string imageUrl);
        Task<IEnumerable<ImageUploadResult>> UploadMultipleImagesAsync(IEnumerable<IFormFile> imageFiles, ImageFolder imageType);
        Task<string> GetImageUrlAsync(string imageName);
    }
}
