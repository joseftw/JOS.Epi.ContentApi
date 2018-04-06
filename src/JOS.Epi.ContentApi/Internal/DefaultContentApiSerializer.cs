using System;
using EPiServer.Core;
using JOS.ContentSerializer;

namespace JOS.Epi.ContentApi.Internal
{
    public class DefaultContentApiSerializer : IContentApiSerializer
    {
        private readonly IContentSerializer _contentSerializer;
        public DefaultContentApiSerializer(IContentSerializer contentSerializer)
        {
            this._contentSerializer = contentSerializer ?? throw new ArgumentNullException(nameof(contentSerializer));
        }

        public string Serialize(IContentData contentData)
        {
            return this._contentSerializer.Serialize(contentData);
        }
    }
}
