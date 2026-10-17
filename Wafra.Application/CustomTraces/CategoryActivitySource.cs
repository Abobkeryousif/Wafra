using System.Diagnostics;

namespace Wafra.Application.CustomTraces
{
    public static class CategoryActivitySource
    {
        public const string SourceName = "wafra.category";
        public static ActivitySource Source = new ActivitySource(SourceName);
             
    }
}
