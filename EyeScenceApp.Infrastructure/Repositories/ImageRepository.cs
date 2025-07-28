using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class ImageRepository : GenericRepository<Image>,IImageRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbContext;

        #endregion

        #region Constructor(s)
        public ImageRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(_dbContext), "ApplicationDbContext cannot be null.");

        }


        #endregion

        #region Method(s)

        public async Task<IEnumerable<Image>> GetAllImagesByCelebirtyIdAsync(Guid Id)
        {
            return await _dbContext.CelebirtyImages.Include(im => im.Image).Where(im => im.CelebirtyId == Id).Select(im=>im.Image).ToListAsync();
        }

        public async Task<Image?> GetPrimaryImagesAsync(Guid Id)
        {
            var result = await _dbContext.CelebirtyImages.Include(im => im.Image).FirstOrDefaultAsync(im => im.CelebirtyId == Id && im.Image.IsPrimary);
            return result.Image;
        }

        public async Task<bool> AddCelebirtyImage(CelebirtyImages image)
        {
             await _dbContext.CelebirtyImages.AddAsync(image);
             return await _dbContext.SaveChangesAsync() >1;
        }
        public async Task<Image?> GetImageById (Guid id , bool asTracking = false)
        {
            return asTracking ?
                                await _dbContext.Image.AsTracking().FirstOrDefaultAsync(img => img.Id == id)
                               : await _dbContext.Image.AsNoTracking().FirstOrDefaultAsync(img => img.Id == id);

        }
        #endregion

    }
}
