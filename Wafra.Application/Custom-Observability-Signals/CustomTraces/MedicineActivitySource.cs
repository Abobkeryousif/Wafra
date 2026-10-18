using System.Diagnostics;

namespace Wafra.Application.Observability.CustomTraces
{
    public class MedicineActivitySource
    {
        public const string SourceName = "wafra.medicine";
        public static ActivitySource Source = new ActivitySource(SourceName);
    }
}
