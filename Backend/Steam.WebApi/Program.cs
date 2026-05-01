using Steam.WebApi.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCore(builder.Configuration);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
