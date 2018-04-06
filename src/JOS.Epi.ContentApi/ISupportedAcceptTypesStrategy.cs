using System.Collections.Generic;

namespace JOS.Epi.ContentApi
{
    public interface ISupportedAcceptTypesStrategy
    {
        IEnumerable<string> Execute();
    }
}
