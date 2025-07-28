using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface ICrewRepository<T> :IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetByNameAsync(string name , bool isTracked = false);
        Task<IEnumerable<DigitalContent>> GetAllWorksAsync(Guid crewId);
    }
}
