using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SloReviewTool.Model
{
    public class SloTarget
    {
        public string TargetValue { get; set; }
        public string Comparator { get; set; }


        public SloTarget(Dictionary<object, object> target)
        {
            TargetValue = target["target-value"] as string;
            Comparator = target["comparator"] as string;
        }

        public static List<SloTarget> ParseList(List<object> targets)
        {
            var list = new List<SloTarget>();
            foreach(Dictionary<object, object> target in targets) {
                list.Add(new SloTarget(target));
            }

            return list;
        }
    }
}
