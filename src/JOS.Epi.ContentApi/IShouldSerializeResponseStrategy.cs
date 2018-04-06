using System.Web;

namespace JOS.Epi.ContentApi
{
    public interface IShouldSerializeResponseStrategy
    {
        bool Execute(HttpRequest request);
    }
}
