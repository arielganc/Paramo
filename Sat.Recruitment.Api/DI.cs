
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Dao;
using Sat.Recruitment.Dao.Interfaces;
using Sat.Recruitment.Service;
using Sat.Recruitment.Service.Interfaces;

namespace Sat.Recruitment.Api
{
    public static class DI
    {
        public static void InitializeInjections(this IServiceCollection services)
        {
            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();
            //********* INYECCION DAOS *******************************
            services.AddTransient<IUserDao, UserDao>();
            //********* FIN INYECCION DAOS *******************************

            //********* INYECCION DAOS *******************************
            services.AddTransient<IUserService, UserService>();
            //********* FIN INYECCION DAOS *******************************

        }
    }
}
