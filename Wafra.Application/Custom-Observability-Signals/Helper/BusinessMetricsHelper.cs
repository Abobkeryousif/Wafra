using System.Diagnostics;
using Wafra.Application.Observability.CustomMetrics;

namespace Wafra.Application.Observability.Helper
{
    public static class BusinessMetricsHelper
    {
        public static void RecordOperation(string module,string operation, string status)
        {
            var tags = new TagList
            {
                {"module", module },
                {"operation", operation },
                {"status", status  }
            };

            WafraBusinessMetrics.OperationsTotal.Add(1, tags);
        }

        public static void RecordDuration(string module, string operation, double durationMs)
        {
            var tags = new TagList
            {
                {"module", module },
                {"operation", operation },
            };

            WafraBusinessMetrics.OperationsDuration.Record(durationMs,tags);
        }
    }
}
