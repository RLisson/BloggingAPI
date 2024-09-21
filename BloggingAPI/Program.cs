using BloggingAPI.Business;
using BloggingAPI.Business.Implementations;
using BloggingAPI.Model.Context;
using BloggingAPI.Repository.Generic;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
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

            // Versioning API
            builder.Services.AddApiVersioning();

            // Dependecy injection
            builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IPostBusiness, PostBusinessImplementation>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            // CORS
            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

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
