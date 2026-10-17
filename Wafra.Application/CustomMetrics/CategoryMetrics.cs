using System.Diagnostics.Metrics;

namespace Wafra.Application.CustomMetrics
{
    public static class CategoryMetrics
    {
        public const string meterName = "wafra.categories";
        private static readonly Meter Meter = new Meter(meterName);

        public static readonly Counter<int> GetAllCategoreis = Meter.CreateCounter<int>(
            "wafra.category.get_all",
            unit: "{call}",
            description: "number of GetAllCategory opration."
            );


        public static readonly Histogram<double> GetAllCategoryDuration = Meter.CreateHistogram<double>(
            "wafra.category.get_all_duration",
            unit: "s",
            description: "duration of GetAllCategory oprations."
            );
    }
}
