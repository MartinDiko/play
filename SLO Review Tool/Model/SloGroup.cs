
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
            Name = SloValidator.GetString(this.GetType().Name, sloGroup, "name");
            State = SloValidator.GetString(this.GetType().Name, sloGroup, "state");
            Slos = Slo.ParseList(SloValidator.GetList(this.GetType().Name, sloGroup, "slos"));
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
