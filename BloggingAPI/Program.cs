using BloggingAPI.Business;
using BloggingAPI.Business.Implementations;
using BloggingAPI.Hypermedia.Enricher;
using BloggingAPI.Hypermedia.Filters;
using BloggingAPI.Model.Context;
using BloggingAPI.Repository.Generic;
using EvolveDb;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Serilog;

namespace BloggingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CORS
            builder.Services.AddCors(option => option.AddDefaultPolicy(b =>
            {
                b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            // Add services to the container.
            builder.Services.AddControllers();

            // DB Connection
            var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
            builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
                connection,
                new MySqlServerVersion(new Version(8, 0, 29))
                ));

            // Migrate DB
            if (builder.Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            // HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PostEnricher());
            builder.Services.AddSingleton(filterOptions);

            // Versioning API
            builder.Services.AddApiVersioning();

            // Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "RESTful Blogging API",
                        Version = "v1",
                        Description = "Blogging API RESTful from roadmap.sh challenge",
                        Contact = new OpenApiContact
                        {
                            Name = "Lucas Risson",
                            Url = new Uri("https://github.com/RLisson")
                        }
                    });
            });

            // Dependecy injection
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IPostBusiness, PostBusinessImplementation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            // CORS
            app.UseCors();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "RESTful Blogging API");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

            app.Run();
        }

        private static void MigrateDatabase(string? connection)
        {
            try
            {
                var evolveConnection = new MySqlConnection(connection);
                var evolve = new Evolve(evolveConnection, Log.Information)
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed ", ex.Message);
                throw;
            }
        }
    }
}
