using EPiServer.Core;

namespace JOS.Epi.ContentApi
{
    public interface IContentApiSerializer
    {
        string Serialize(IContentData contentData);
    }
}
