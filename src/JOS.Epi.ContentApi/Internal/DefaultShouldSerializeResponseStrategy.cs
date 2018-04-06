using System;
using System.IO;
using System.Linq;
using System.Web;
using EPiServer.ServiceLocation;

namespace JOS.Epi.ContentApi.Internal
{
    public class DefaultShouldSerializeResponseStrategy : IShouldSerializeResponseStrategy
    {
        public bool Execute(HttpRequest request)
        {
            if (request.Url.IsFile || HasFileExtension(request.Path))
            {
                return false;
            }

            var acceptHeaders = request.AcceptTypes ?? Array.Empty<string>();
            var supportedAcceptTypesStrategy = ServiceLocator.Current.GetInstance<ISupportedAcceptTypesStrategy>();
            var supportedAcceptTypes = supportedAcceptTypesStrategy.Execute();
            return acceptHeaders.Length == 1 && supportedAcceptTypes.Contains(acceptHeaders.First(), StringComparer.OrdinalIgnoreCase);
        }

        private static bool HasFileExtension(string path)
        {
            return Path.HasExtension(path);
        }
    }
}
