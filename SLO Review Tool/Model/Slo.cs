using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SloReviewTool.Model
{
    public class Slo
    {
        public string Name { get; set; }
        public string SourceId { get; set; }
        public string Description { get; set; }
        public string Namespace { get; set; }
        public string Signal { get; set; }
        public string Window { get; set; }
        public List<SloTarget> Targets { get; set; }

        public Slo(Dictionary<object, object> slo)
        {
            Name = slo["name"] as string;
            SourceId = slo["source-id"] as string;
            Description = slo["description"] as string;
            Namespace = slo["namespace"] as string;
            Signal = slo["signal"] as string;
            Window = slo["window"] as string;
            Targets = SloTarget.ParseList(slo["targets"] as List<object>);
        }

        public static List<Slo> ParseList(List<object> slos)
        {
            var list = new List<Slo>();
            foreach (Dictionary<object, object> slo in slos) {
                list.Add(new Slo(slo));
            }

            return list;
        }
    }
}
