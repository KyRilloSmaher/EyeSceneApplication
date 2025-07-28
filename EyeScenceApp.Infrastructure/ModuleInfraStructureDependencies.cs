using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using EyeScenceApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EyeScenceApp.Infrastructure
{
    public static class ModuleInfraStructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
       

            // Register Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IAwardRepository,AwardRepository>();
            services.AddTransient<ICelebirtyRepository, CelebirtyRepository>();
            services.AddTransient<ICastRepository, CastRepository>();
            services.AddTransient<IDirectorRepository, DirectorRepository>();
            services.AddTransient<IEditorRepository, EditorRepository>();
            services.AddTransient<IWriterRepository, WritersRepository>();
            services.AddTransient<IProducerRepository , ProducerRepository>();
            services.AddTransient<ISoundDeignerRepository, SoundDesignerRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IRatingRepository, RateRepository>();
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<IEpisodeRepository, EpisodeRepository>();
            services.AddTransient<ISingleDocumentaryRepository, SingleDocumentaryRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            services.AddTransient<IWatchListRepository, WatchListRepository>();
            services.AddTransient<IApplicationUserRepository, UserRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IDigitalContentRepository, DigitalContentRepository>();
            services.AddTransient<IRefreshTokenRepository , ResfreshTokenRepository>();


            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }

    }
}
