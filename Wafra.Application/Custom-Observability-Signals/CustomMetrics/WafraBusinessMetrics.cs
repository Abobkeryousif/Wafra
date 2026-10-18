
using System.Diagnostics.Metrics;

namespace Wafra.Application.Observability.CustomMetrics
{
    public static class WafraBusinessMetrics
    {
        public const string meterName = "Wafra.Business";

        private readonly static Meter Meter = new Meter(meterName);

        public static readonly Counter<long> OperationsTotal = Meter.CreateCounter<long>(
                "wafra.business.operations",
                unit: "{operation}",
                description: "Total number of business operations executed"
            );


        public static Histogram<double> OperationsDuration = Meter.CreateHistogram<double>(
                "wafra.business.operation.duration",
                unit: "{s}",
                description: "duration of business operation in seconds"
            );
    }
}
