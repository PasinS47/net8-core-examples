using api.Data;
using api.Interfaces;
using api.Repository;
using DotNetEnv;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;

var envPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "..", ".env"));

if (File.Exists(envPath))
{
    Env.Load(envPath);
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson( option =>
{
   option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
});

var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "database";
var uid = Environment.GetEnvironmentVariable("DB_UID") ?? "root";
var pwd = Environment.GetEnvironmentVariable("DB_PWD") ?? "1234";

var connectionString = $"Server={server};port=3306;Database={database};Uid={uid};Pwd={pwd}";

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
