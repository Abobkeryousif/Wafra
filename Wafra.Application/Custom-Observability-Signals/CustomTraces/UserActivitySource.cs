using System.Diagnostics;

namespace Wafra.Application.Custom_Observability_Signals.CustomTraces
{
    public static class UserActivitySource
    {
        public const string SourceName = "wafra.user";

        public static ActivitySource Source = new ActivitySource(SourceName);
    }
}
