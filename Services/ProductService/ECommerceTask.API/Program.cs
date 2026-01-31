using ECommerceTask.Infrastructure.Data;
using ECommerceTask.Infrastructure.Repositories;
using ECommerceTask.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerceTask.Application;
using ECommerceTask.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddApplicationRegistration();

var redisConfig = builder.Configuration["Redis:Url"] ?? "localhost:6379";

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig;
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
    {
        EndPoints = { redisConfig },
        AbortOnConnectFail = false,
        ConnectTimeout = 5000
    };
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddMassTransit(x =>
{
    var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "localhost";
    var rabbitMqUser = builder.Configuration["RabbitMq:Username"] ?? "guest";
    var rabbitMqPass = builder.Configuration["RabbitMq:Password"] ?? "guest";

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUser);
            h.Password(rabbitMqPass);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ECommerceDbContext>();
        context.Database.Migrate();
        Console.WriteLine("--> Veritabaný Migration iþlemi baþarýyla tamamlandý.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("--> Migration sýrasýnda hata oluþtu: " + ex.Message);
    }
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();