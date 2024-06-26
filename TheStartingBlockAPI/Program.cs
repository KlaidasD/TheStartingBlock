using TheStartingBlock.Data;
using TheStartingBlock.Models;
using TheStartingBlock.Repositories;
using TheStartingBlock.Services;
using MongoDB.Driver;
using Serilog;
using TheStartingBlock.Contracts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>();

//Repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();



//Services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddSingleton<CacheControlService>();


builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

builder.Services.AddSingleton<IMongoRepository, MongoRepository>(sp => new MongoRepository(sp.GetRequiredService<IMongoClient>()));

var log = new LoggerConfiguration()
.WriteTo.Console()
.WriteTo.File("logs/TheStartingBlockAPI.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();
Log.Logger = log;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var cacheControlService = app.Services.GetRequiredService<CacheControlService>();
_ = cacheControlService.RunCleanupJob();

app.Run();
