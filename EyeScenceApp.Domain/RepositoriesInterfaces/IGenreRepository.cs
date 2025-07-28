using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        public Task<bool> IsvalidGenre(byte id);
        public Task<Genre> GetByIdAsync(byte Id, bool asTracked = false);

        public Task<Genre> GetByNameAsync(string name, bool asTracked = false);
    }
}
