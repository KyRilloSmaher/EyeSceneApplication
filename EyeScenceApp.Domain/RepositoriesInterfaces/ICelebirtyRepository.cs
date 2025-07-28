using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface ICelebirtyRepository : IGenericRepository<Celebrity>
    {
        public Task<IEnumerable<Celebrity>> GetCelebritiesByNameAsync(string name);
    }
}
