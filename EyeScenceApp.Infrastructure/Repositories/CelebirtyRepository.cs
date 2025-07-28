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
    public class CelebirtyRepository : GenericRepository<Celebrity>, ICelebirtyRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Constructor(s)
        public CelebirtyRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext ?? throw new ArgumentNullException(nameof(_dbContext), "ApplicationDbContext cannot be null.");

        }

        #endregion

        #region Method(s)


        public async Task<IEnumerable<Celebrity>> GetCelebritiesByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }

            return await _dbContext.Celebrities
                                   .Where(c => c.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                                c.LastName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                                c.MiddleName.Contains(name, StringComparison.OrdinalIgnoreCase))
                                   .ToListAsync();
        }
        #endregion
    }
}
