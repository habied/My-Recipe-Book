using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Core;
using RecipeBook.Core.DTOs;
using RecipeBook.Core.Interfaces;
using RecipeBook.Core.Validators;
using RecipeBook.EF;

namespace RecipeBookApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            builder.Services.AddDbContext<RecipeBookDBContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(RecipeBookDBContext).Assembly.FullName)));

            builder.Services.AddScoped<IRecipeUOW, RecipeUOW>();
            builder.Services.AddTransient<IValidator<RecipeDTO>, RecipeDTOValidator>();
            builder.Services.AddTransient<IValidator<RecipeIngredientDTO>, RecipeIngredientDTOValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
