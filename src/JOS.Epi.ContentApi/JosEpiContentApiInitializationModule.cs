using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using JOS.Epi.ContentApi.Internal;

namespace JOS.Epi.ContentApi
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class JosEpiContentApiInitializationModule : IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var events = ServiceLocator.Current.GetInstance<IContentRouteEvents>();
            events.RoutedContent += OnRoutedContent;
        }

        private void OnRoutedContent(object sender, RoutingEventArgs e)
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null)
            {
                return;
            }
            var shouldSerializeStrategy = ServiceLocator.Current.GetInstance<IShouldSerializeResponseStrategy>();
            if (!shouldSerializeStrategy.Execute(httpContext.Request))
            {
                return;
            }

            var contentLink = e.RoutingSegmentContext.RoutedContentLink;
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            contentLoader.TryGet<IContent>(contentLink, out var content);

            if (content == null)
            {
                return;
            }

            e.CancelFurtherRouting = true;
            var serializer = ServiceLocator.Current.GetInstance<IContentApiSerializer>();
            var result = serializer.Serialize(content);

            httpContext.Response.ContentType = httpContext.Request.AcceptTypes?.First();
            httpContext.Response.Write(result);
            httpContext.Response.Flush();
            httpContext.Response.End();
        }

        public void Uninitialize(InitializationEngine context) {}

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ISupportedAcceptTypesStrategy, DefaultSupportedAcceptTypesStrategy>();
            context.Services.AddSingleton<IContentApiSerializer, DefaultContentApiSerializer>();
            context.Services.AddSingleton<IShouldSerializeResponseStrategy, DefaultShouldSerializeResponseStrategy>();
        }
    }
}