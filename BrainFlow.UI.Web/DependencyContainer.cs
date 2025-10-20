using BrainFlow.Repository.Interfaces;
using BrainFlow.Repository.Repositories;

namespace BrainFlow.UI.Web
{
    public class DependencyContainer
    {
        public static void RegisterContainers(IServiceCollection services)
        {
            // Removidas todas as dependências de repositório para evitar conexão com banco
            // services.AddScoped<IUsuarioREP, UsuarioREP>();
            // services.AddScoped<IEmailService, EmailService>();
            // services.AddScoped<IAfiliadoREP, AfiliadoREP>();
            // services.AddScoped<ICursoREP, CursoREP>();
            // services.AddScoped<IModuloREP, ModuloREP>();
        }
    }
}
