using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineCinema.API.DI;
using OnlineCinema.DataLayer;
using OnlineCinema.Shared;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();
        var Configuration = builder.Configuration;
        // Add services to the container.
        builder.Services.AddDbContext<CinemaDBContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddDI(Configuration);
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // вказує, чи валідуватиметься видавець при валідації токена
                    ValidateIssuer = true,
                    // Рядок, що представляє видавця
                    ValidIssuer = AuthOptions.ISSUER,
                    // Чи валідуватиметься споживач токена
                    ValidateAudience = true,
                    // Установка споживача токена
                    ValidAudience = AuthOptions.AUDIENCE,
                    // чи валідуватиметься час існування
                    ValidateLifetime = true,
                    // встановлення ключа безпеки
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    // валідація ключа безпеки
                    ValidateIssuerSigningKey = true,
                };
            });
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

}