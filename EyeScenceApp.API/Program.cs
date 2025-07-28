using EyeScenceApp.API.Helpers;
using EyeScenceApp.API.Middlewares;
using EyeScenceApp.Application;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Infrastructure;
using EyeScenceApp.Infrastructure.DbContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


    //Swagger Gn
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eye_Scene_Application_API ", Version = "v1" });
        c.EnableAnnotations();

        c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme

        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                 {
                 new OpenApiSecurityScheme
                 {
                     Reference = new OpenApiReference
                     {
                         Type = ReferenceType.SecurityScheme,
                         Id = JwtBearerDefaults.AuthenticationScheme
                     }
                 },
                 Array.Empty<string>()
                 }
               });
    });


#region Connection To SQL SERVER
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<ApplicationDbContext>(opt => { opt.UseSqlServer(connectionString); });

#endregion
#region Dependency injections
builder.Services .AddInfrastructureDependencies()
                 .AddApplicationDependencies(builder.Configuration);
//                 .AddServiceRegisteration(builder.Configuration);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUrlHelper>(x =>
    x.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));


#endregion
#region Serilog
//Log.Logger = new LoggerConfiguration()
//              .ReadFrom.Configuration(builder.Configuration).CreateLogger();
//builder.Services.AddSerilog();
builder.Services.AddLogging();
#endregion
#region AllowCORS
var CORS = "_DefaultCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});

#endregion
#region  Register-FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining(typeof(CreateActorDTO));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );

        var apiResponse = Router.FinalResponse(
         new Response<bool> {
          StatusCode = System.Net.HttpStatusCode.BadRequest,
          ErrorsBag = errors,
          Data = false,
          Message = "Validation Errors !" 
         }    
        );

        return apiResponse;
    };
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();