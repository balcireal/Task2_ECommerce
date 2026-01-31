using MassTransit;
using LogService.API.Consumers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


var seqUrl = builder.Configuration["Seq:Url"] ?? "http://localhost:5341";
var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "localhost";
var rabbitMqUser = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPass = builder.Configuration["RabbitMq:Password"] ?? "guest";


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.Seq(seqUrl)
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUser);
            h.Password(rabbitMqPass);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();