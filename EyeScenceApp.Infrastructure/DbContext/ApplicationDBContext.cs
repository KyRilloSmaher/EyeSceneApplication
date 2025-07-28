using EyeScenceApp.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EyeScenceApp.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets for all entities
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Celebrity> Celebrities { get; set; }
        public DbSet<CelebirtyAward> CelebirtyAwards { get; set; }
        public DbSet<CelebirtyImages> CelebirtyImages { get; set; }
        public DbSet<CelebirtyAwards> CelebirtyAwardsJoin { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<DigitalContent> DigitalContents { get; set; }
        public DbSet<DigitalContentAward> DigitalContentAwards { get; set; }
        public DbSet<DigitalContentAwards> DigitalContentAwardsJoin { get; set; }
        public DbSet<DigitalContentGenres> DigitalContentGenres { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SingleDocumentary> SingleDocumentaries { get; set; }
        public DbSet<SoundDesigner> SoundDesigners { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<WorksOn> WorksOn { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<DigitalContentAward> DigitalContentAwardEntities { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DigitalContent>().ToTable("DigitalContents");
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Series>().ToTable("Series");
            modelBuilder.Entity<SingleDocumentary>().ToTable("SingleDocumentary");
            modelBuilder.Entity<DigitalContentAward>().ToTable("DigitalContentAward");
            modelBuilder.Entity<CelebirtyAward>().ToTable("CelebirtyAward");
            modelBuilder.Entity<Cast>().ToTable("Casts");
            modelBuilder.Entity<Crew>().ToTable("Crews");
            modelBuilder.Entity<Writer>().ToTable("Writers");
            modelBuilder.Entity<Director>().ToTable("Directors");
            modelBuilder.Entity<Editor>().ToTable("Editors");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            modelBuilder.Entity<SoundDesigner>().ToTable("SoundDesigners");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}