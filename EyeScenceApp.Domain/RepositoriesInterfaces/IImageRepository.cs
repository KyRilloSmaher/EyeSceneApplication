using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IImageRepository:IGenericRepository<Image>
    {
        public Task<IEnumerable<Image>> GetAllImagesByCelebirtyIdAsync(Guid Id);
        public Task<Image?> GetPrimaryImagesAsync(Guid Id);
        public Task<Image?> GetImageById(Guid id, bool asTracking = false);
        public Task<bool> AddCelebirtyImage (CelebirtyImages image);
    }
}
