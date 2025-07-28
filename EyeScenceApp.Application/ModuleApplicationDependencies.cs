using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Application.Services;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace EyeScenceApp.Application
{
    public static class ModuleApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Application Services 
            services.AddTransient<IImageUploaderService, ImageUploaderService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton<ResponseHandler>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IActorService, ActorService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IFavoritesService, FavoritesService>();
            services.AddTransient<IWatchListService, WatchListService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IDigitalContentService, DigitalContentService>();
            services.AddTransient<ISingleDocumantryService, SingleDocumantryService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<ISeriesService, SeriesService>();
            services.AddTransient<IAwardService, AwardService>();
            services.AddTransient<IUserService ,UserService>();
            services.AddTransient<IDirectorService, DirectorService>();
            services.AddTransient<IEditorService, EditorService>();
            services.AddTransient<IWriterService, WriterService>();
            services.AddTransient<IProducerService, ProducerService>();
            services.AddTransient<ISoundDesignerService, SoundDesignerService>();



            // Register Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register CloudinaryService
            var cloudinary = new Cloudinaryx();
            configuration.GetSection("cloudinary").Bind(cloudinary);
            services.AddSingleton(cloudinary);


            // Email
            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(emailSettings);

            //JWT Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = jwtSettings.ValidateIssuer,
                   ValidIssuers = new[] { jwtSettings.Issuer },
                   ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                   ValidAudience = jwtSettings.Audience,
                   ValidateAudience = jwtSettings.ValidateAudience,
                   ValidateLifetime = jwtSettings.ValidateLifeTime,
               };
           });
            return services;
        }

    }
}
