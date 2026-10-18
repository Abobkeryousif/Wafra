
using System.Diagnostics;

namespace Wafra.Application.Custom_Observability_Signals.CustomTraces
{
    public static class PharmacyActivitySource
    {
        public const string SourceName = "wafra.pharmacy";
        public static ActivitySource Source = new ActivitySource(SourceName);
    }
}
