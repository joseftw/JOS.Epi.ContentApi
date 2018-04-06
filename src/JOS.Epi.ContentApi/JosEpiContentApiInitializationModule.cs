using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using JOS.Epi.ContentApi.Internal;

namespace JOS.Epi.ContentApi
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class JosEpiContentApiInitializationModule : IConfigurableModule
    {
        public void Initialize(InitializationEngine context) {}

        public void Uninitialize(InitializationEngine context) {}

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ISupportedAcceptTypesStrategy, DefaultSupportedAcceptTypesStrategy>();
            context.Services.AddSingleton<IContentApiSerializer, DefaultContentApiSerializer>();
            context.Services.AddSingleton<IShouldSerializeResponseStrategy, DefaultShouldSerializeResponseStrategy>();
        }
    }
}