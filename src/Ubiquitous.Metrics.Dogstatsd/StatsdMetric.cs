using System.Linq;

namespace Ubiquitous.Metrics.Dogstatsd {
    abstract class StatsdMetric {
        readonly string[] _labelNames;

        protected string MetricName { get; }

        protected StatsdMetric(MetricDefinition metricDefinition) {
            _labelNames = metricDefinition.LabelNames;
            MetricName  = metricDefinition.Name;
        }

        protected string[]? FormTags(string[]? labels) => StatsTags.FormTags(_labelNames, labels);
    }

    static class StatsTags {
        internal static string[]? FormTags(string[]? labelNames, string[]? labelValues) {
            return labelNames?.Select(FormTag).ToArray();

            string FormTag(string tag, int position) {
                var label = IsInvalidLabel(position)
                    ? null
                    : $":{labelValues![position]}";

                return $"{tag}{label}";
            }

            bool IsInvalidLabel(int position) => labelValues == null || labelValues.Length <= position || string.IsNullOrEmpty(labelNames[position]);
        }
    }
}
