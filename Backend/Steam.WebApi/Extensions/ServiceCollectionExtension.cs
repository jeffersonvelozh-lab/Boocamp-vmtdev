using Steam.Application.Interfaces.Services;
using Steam.Application.Services;
using Steam.Domain.Database.SqlServer.Context;
using Steam.Domain.Interfaces.Repositories;
using Steam.Infrastructure.Persistence.SqlServer.Repositories;

namespace Steam.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Método que sirve para añadir los servicios de la aplicación
        /// </summary>
        /// <param name="service"></param>
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();

        }


        /// <summary>
        /// Método que sirve para añadir los repositorios de la aplicación
        /// </summary>
        /// <param name="service"></param>
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddTransient<IUserRepository, UserRepository>();

        }


        /// <summary>
        /// Método que sirve para añadir lo esencial para que la api funcione
        /// </summary>
        /// <param name="service"></param>
        public static void AddCore(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            service.AddOpenApi();

            //DataBase
            service.AddSqlServer<SteamCloneContext>(configuration.GetConnectionString("Database"));
            service.AddRepositories();

            service.AddServices();


        }
    }
}
