using System;
using System.IO;
using System.Linq;
using System.Web;
using EPiServer.ServiceLocation;

namespace JOS.Epi.ContentApi.Internal
{
    public class DefaultShouldSerializeResponseStrategy : IShouldSerializeResponseStrategy
    {
        private readonly ISupportedAcceptTypesStrategy _supportedAcceptTypesStrategy;

        public DefaultShouldSerializeResponseStrategy(ISupportedAcceptTypesStrategy supportedAcceptTypesStrategy)
        {
            _supportedAcceptTypesStrategy = supportedAcceptTypesStrategy ?? throw new ArgumentNullException(nameof(supportedAcceptTypesStrategy));
        }

        public bool Execute(HttpRequest request)
        {
            if (request.Url.IsFile || HasFileExtension(request.Path))
            {
                return false;
            }

            var acceptHeaders = request.AcceptTypes ?? Array.Empty<string>();
            var supportedAcceptTypes = this._supportedAcceptTypesStrategy.Execute();
            return acceptHeaders.Length == 1 && supportedAcceptTypes.Contains(acceptHeaders.First(), StringComparer.OrdinalIgnoreCase);
        }

        private static bool HasFileExtension(string path)
        {
            return Path.HasExtension(path);
        }
    }
}
