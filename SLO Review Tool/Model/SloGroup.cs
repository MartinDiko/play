
using System.Collections.Generic;


namespace SloReviewTool.Model
{
    public class SloGroup
    {
        public string Name { get; set; }
        public string State { get; set; }
        public List<Slo> Slos { get; set; }

        public SloGroup(Dictionary<object, object> sloGroup)
        {
            Name = sloGroup["name"] as string;
            State = sloGroup["state"] as string;
            Slos = Slo.ParseList(sloGroup["slos"] as List<object>);
        }

        public static List<SloGroup> ParseList(List<object> sloGroups)
        {
            var list = new List<SloGroup>();
            foreach (Dictionary<object, object> sloGroup in sloGroups) {
                list.Add(new SloGroup(sloGroup));
            }

            return list;
        }
    }
}
