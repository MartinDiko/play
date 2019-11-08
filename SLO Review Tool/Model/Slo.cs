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
            Name = SloValidator.GetString(this.GetType().Name, slo, "name");
            SourceId = SloValidator.GetString(this.GetType().Name, slo, "source-id");
            Description = SloValidator.GetString(this.GetType().Name, slo, "description");
            Namespace = SloValidator.GetString(this.GetType().Name, slo, "namespace");
            Signal = SloValidator.GetString(this.GetType().Name, slo, "signal");
            Window = SloValidator.GetString(this.GetType().Name, slo, "window");
            Targets = SloTarget.ParseList(SloValidator.GetList(this.GetType().Name, slo, "targets"));
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
