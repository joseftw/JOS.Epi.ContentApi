using System.Collections.Generic;

namespace JOS.Epi.ContentApi.Internal
{
    public class DefaultSupportedAcceptTypesStrategy : ISupportedAcceptTypesStrategy
    {
        private static readonly IEnumerable<string> DefaultAcceptTypes = new List<string> {"application/json"}; 
        public IEnumerable<string> Execute()
        {
            return DefaultAcceptTypes;
        }
    }
}
