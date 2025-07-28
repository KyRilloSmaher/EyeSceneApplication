using CloudinaryDotNet;
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
    public class MovieRepository : GenericRepository<Movie>,IMovieRepository
    {
        #region Field(s)
        private readonly ApplicationDbContext _dbcontext;
        private readonly IRatingRepository _ratingRepository;
        #endregion

        #region Constructor(s)
        public MovieRepository(ApplicationDbContext context, IRatingRepository ratingRepository) : base(context)
        {
            _dbcontext = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
            _ratingRepository = ratingRepository ?? throw new ArgumentNullException(nameof(ratingRepository), "RatingRepository cannot be null.");
        }


        #endregion

        #region Method(s)

        public async override Task<Movie?> GetByIdAsync(Guid id, bool AsTracking = false)
        {
            return AsTracking ?
                                await _dbcontext.Movies.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                       .Include(sd => sd.Image).AsTracking().FirstOrDefaultAsync(dg => dg.Id == id && dg is Movie) as Movie
                              : await _dbcontext.Movies.Include(sd => sd.Genres).ThenInclude(g => g.Genre)
                                       .Include(sd => sd.Image).AsNoTracking().FirstOrDefaultAsync(dg => dg.Id == id && dg is Movie) as Movie;
        }

        public async Task<IEnumerable<Movie?>> GetMoviesByGenreIdAsync(byte genreId)
        {
            if (genreId == 0)
            {
                throw new ArgumentException("Genre ID cannot be zero.", nameof(genreId));
            }

            return await _dbcontext.DigitalContents
                                    .Include(sd => sd.Genres)
                                        .ThenInclude(g => g.Genre)
                                    .Include(sd => sd.Image)
                                   .Where(dg => dg.Genres.Any(genre=>genre.GenreId == genreId) && dg is Movie)
                                   .Select(dg => dg as Movie)
                                   .ToListAsync();
        }

        public async Task<IEnumerable<Movie?>> GetMoviesByTitleAsync(string title)
        {
            return await _dbcontext.Movies
                .Include(sd => sd.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(sd => sd.Image)
                .Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie?>> GetNewAddedMoviesAsync(byte genreId = 0)
        {
            // Calculate the date one month ago from today
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            IQueryable<Movie> query = _dbcontext.Movies
                .Include(sd => sd.Genres)
                    .ThenInclude(g => g.Genre)
                .Include(sd => sd.Image)
                .Where(m => m.UploadingDate >= oneMonthAgo); // Only include movies from last month

            if (genreId > 0)
            {
                query = query.Where(m => m.Genres.Any(g => g.GenreId == genreId));
            }

            return await query.OrderByDescending(m => m.UploadingDate)
                             .Take(10)
                             .ToListAsync();
        }
        public async Task<IEnumerable<Movie?>> GetMoviesOrderedByReveuesAsync(int topCount = 10)
        {
            return  await _dbcontext.Movies
                                             .Include(sd => sd.Genres)
                                                .ThenInclude(g => g.Genre)
                                            .Include(sd => sd.Image)
                                            .OrderByDescending(sd=>sd.Revenues)
                                            .ToListAsync();
        }
        public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int topCount = 10)
        {
            var movies = await _dbcontext.Movies
                                             .Include(sd => sd.Genres)
                                                .ThenInclude(g => g.Genre)
                                            .Include(sd => sd.Image)
                                            .ToListAsync();
            foreach (var movie in movies)
            {
                movie.Rate = _ratingRepository.GetRatesOfDigitalContentAsync(movie.Id);
            }
            return movies.OrderByDescending(x => x.Rate).Take(topCount);
        }

        #endregion
    }
}
