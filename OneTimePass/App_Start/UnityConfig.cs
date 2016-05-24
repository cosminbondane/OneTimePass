using Microsoft.Practices.Unity;
using OneTimePass.Audit;
using OneTimePass.Business;
using OneTimePass.Data;
using OneTimePass.DataAccess;
using OneTimePass.Util.Generator;
using System.Web.Http;
using Unity.WebApi;

namespace OneTimePass
{
    public static class UnityConfig
    {
        private static UnityContainer container = new UnityContainer();

        public static void RegisterComponents()
        {
            container.RegisterType<IPasswordGenerator, PasswordGenerator>();
            container.RegisterType<IAuditLogger, AuditLogger>();
            container.RegisterType<IRepository<Account>, AccountMockRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}