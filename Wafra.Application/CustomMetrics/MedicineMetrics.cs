
using System.Diagnostics.Metrics;

namespace Wafra.Application.CustomMetrics
{
    public static class MedicineMetrics
    {
        public const string meterName = "wafra.medicine";

        private static readonly Meter Meter = new Meter(meterName);

        public static readonly Counter<int> GetAllMedicine = Meter.CreateCounter<int>(
            "wafra.medicine.get_all",
            unit: "{call}",
            description: "number of get all medicine opration"
            );


        public static readonly Histogram<double> GetAllMedicineDuration = Meter.CreateHistogram<double>(
            "wafra.medicine.get_all_duration",
            unit: "{s}",
            description: "duration of get all category opration"
            );
    }
}
