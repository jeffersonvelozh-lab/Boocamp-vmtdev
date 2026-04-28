using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Services;
using Steam.Domain.Database.SqlServer.Context;
using Steam.Domain.Interfaces.Repositories;
using Steam.Infrastructure.Persistence.SqlServer.Repositories;
using Steam.Shared.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<Cache<UserDto>>();

//DataBase
builder.Services.AddSqlServer<SteamCloneContext>(builder.Configuration.GetConnectionString("Database"));

//Database /Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
