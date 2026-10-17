using System.Diagnostics;

namespace Wafra.Application.CustomTraces
{
    public class MedicineActivitySource
    {
        public const string SourceName = "wafra.medicine";
        public static ActivitySource Source = new ActivitySource(SourceName);
    }
}
