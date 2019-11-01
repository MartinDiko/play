using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloReviewTool.Model
{
    public class SloDefinition
    {
        Dictionary<string, object> slo_;

        public SloDefinition(string jsonText)
        {
            slo_ = ParseSloText(jsonText);
        }

        public string SloText { get; private set; }

        public string ServiceId {
            get {
                return slo_["service-id"].ToString();
            }
        }

        Dictionary<string, object> ParseSloText(string jsonText)
        {
            // For now, parse JSON to get SLO yaml.  Service Tree should be fixed.
            var json = JObject.Parse(jsonText);
            SloText = json["ServiceLevelObjectives"].ToString();

            var deserializer = new YamlDotNet.Serialization.Deserializer();
            return deserializer.Deserialize<Dictionary<string, object>>(SloText);
        }

    }
}
