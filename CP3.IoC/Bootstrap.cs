using CP3.Application.Services;
using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP3.IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext
            services.AddDbContext<ApplicationContext>(x =>
            {
                x.UseOracle(configuration["ConnectionStrings:Oracle"]);
            });

            // Injeção de dependência das interfaces e implementações
            services.AddScoped<IBarcoRepository, BarcoRepository>();
            services.AddScoped<IBarcoApplicationService, BarcoApplicationService>();
        }
    }
}
