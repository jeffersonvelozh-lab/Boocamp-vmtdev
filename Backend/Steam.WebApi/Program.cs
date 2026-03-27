using Steam.Application.Interfaces.Services;
using Steam.Application.Models.Dtos;
using Steam.Application.Services;
using Steam.Shared.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<Cache<UserDto>>();


var app = builder.Build();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
