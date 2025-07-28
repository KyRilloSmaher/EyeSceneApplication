using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface ISingleDocumentaryRepository : IGenericRepository<SingleDocumentary>
    {
        public Task<IEnumerable<SingleDocumentary>> GetSingleDocumentaryByTitleAsync(string title);
        public Task<IEnumerable<SingleDocumentary>> GetSingleDocumentariesByGenreIdAsync(byte genreId);
        public Task<IEnumerable<SingleDocumentary>> GetNewAddedSingleDocumentaries(byte genreId = 0);
        public Task<IEnumerable<SingleDocumentary>> GetTopRatedSingleDocumantaryAsync(int topCount = 10);
    }
}
